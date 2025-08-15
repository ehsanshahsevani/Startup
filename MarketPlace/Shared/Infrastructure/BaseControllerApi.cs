using Domain;
using AutoMapper;
using Persistence;
using FluentResults;
using Enums.Marketplace;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Base;
using HttpServices.AttachmentManager;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class BaseControllerApi : ControllerSeedworks.BaseApiController
{
    public IUnitOfWork UnitOfWork { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }

    public BaseControllerApi(
        IMapper mapper, HttpClient httpClient, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
        : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
    {
        UnitOfWork = unitOfWork;
        HttpContextAccessor = httpContextAccessor;
    }

    [NonAction]
    protected string GetCurrentUserId()
    {
        // دریافت توکن از هدر درخواست
        var authorizationHeader = HttpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return null;
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                              ?? jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            return userIdClaim;
        }
        catch (SecurityTokenException)
        {
            // در صورت وجود مشکل در اعتبارسنجی توکن
            return null;
        }
    }

    // متد جایگزین برای دسترسی مستقیم به UserId از HttpContext
    [NonAction]
    protected string? GetUserIdFromClaims()
    {
        return HttpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
               ?? HttpContextAccessor.HttpContext?.User?.FindFirstValue("nameid");
    }

    [NonAction]
    protected async Task<Result<Attachment>>
        UploadAndCreateAttachmentByFormFileAsync(
            IFormFile file, AttachmentSubjectEnum attachmentSubjectEnum,
            string subSystemName, string relationId, string? description = null)
    {
        var result = new Result<Attachment>();

        var service =
            new AttachmentService();

        const string serverId = Domain.Base.ServerKeyConstant.Key;

        var resultSaveFile = await service
            .UploadAsync(file, serverId, subSystemName);

        if (resultSaveFile.IsSuccess == false)
        {
            result.WithErrors(resultSaveFile.Errors);
            return result;
        }

        var uploadValue = resultSaveFile.Value;

        if (uploadValue is null)
        {
            return result;
        }

        var subSystemLocal =
            await UnitOfWork
                .SubSystemLocalRepository
                .FindByNameAsync(subSystemName);

        var attachmentSubject =
            await UnitOfWork.AttachmentSubjectRepository
                .FindByAttachmentSubjectEnumAsync(attachmentSubjectEnum);

        if (subSystemLocal is null)
        {
            throw new NullReferenceException(nameof(subSystemLocal));
        }

        var attachment = new Attachment
        {
            RelationId = relationId,
            Description = description,
            FileName = uploadValue.FileName,
            SubSystemLocalId = subSystemLocal.Id,
            AttachmentSubjectId = attachmentSubject.Id,
            AttachmentSubjectEnum = attachmentSubjectEnum,
            FileOriginalName = uploadValue.FileOriginalName,
            FileThumbnailName = uploadValue.FileThumbnailName,
        };

        result.WithValue(attachment);

        return result;
    }

    [NonAction]
    protected async Task<Result> RemoveAttachmentAsync(string subSystemName, string id)
    {
        AttachmentService service = new();
        Result result = new();

        var files = await UnitOfWork.AttachmentRepository
            .FindAllAttachmentsByIdsAndSubSystemName([id],subSystemName);

        foreach (var file in files)
        {
            if (file.FileIsExist())
            {
                var resultSaveFile = await service.DeleteAsync
                    (file.FileName!, ServerKeyConstant.Key);

                if (resultSaveFile.IsSuccess)
                {
                    await UnitOfWork.AttachmentRepository.RemoveByIdAsync(file.Id);
                }

                if (resultSaveFile.IsFailed)
                {
                    result.WithErrors(resultSaveFile.Errors);
                }
            }
        }

        return result;
    }

    [NonAction]
    protected async Task FetchAndSetAttachmentAsync<TList,TResponse>(string subSystemName,
        List<TList> values, bool isAttachmentRequired = true)
        where TList : ViewmodelSeedworks.Response.BaseResponseViewModelWithImage<TResponse>
    where TResponse : ViewmodelSeedworks.Request.BaseRequestViewModel
    {
        var ids = values.Select(p => p.Id).ToList();

        var attachments = await UnitOfWork.AttachmentRepository
            .FindAllAttachmentsByIdsAndSubSystemName(ids!, subSystemName);

        foreach (var attachment in attachments)
        {
            if (isAttachmentRequired == false)
            {
                continue;
            }

            var value = values.First(p => p.Id == attachment.RelationId);
            value.SetAttachmentInThisResponse(attachment);
        }
    }
}