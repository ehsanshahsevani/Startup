using System;
using AutoMapper;
using FluentResults;
using Enums.Marketplace;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PersistenceSeedworks.LogManager;
using Microsoft.Extensions.Configuration;
using Attachment = Domain.Attachment;
using IUnitOfWork = Persistence.IUnitOfWork;

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
    protected string? GetUserIdFromClaims()
    {
        return HttpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
               ?? HttpContextAccessor.HttpContext?.User?.FindFirstValue("nameid");
    }

    protected async Task<Result<Attachment>>
        UploadAndCreateAttachmentByFormFileAsync(
            IFormFile file, AttachmentSubjectEnum attachmentSubjectEnum,
            string subSystemName, string relationId, string? description = null)
    {
        var result = new Result<Attachment>();

        var service =
            new HttpServices.AttachmentManager.AttachmentService();

        const string serverId = Domain.Base.ServerKeyConstant.Key;

        var resultSaveFile = await service.UploadAsync(file, serverId, subSystemName);

        if (resultSaveFile.IsFailed == true)
        {
            result.WithErrors(resultSaveFile.Errors);
        }

        if (resultSaveFile.IsSuccess == false)
        {
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
}