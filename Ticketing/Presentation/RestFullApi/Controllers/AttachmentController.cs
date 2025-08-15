using AutoMapper;
using Constants;
using Domain;
using Domain.Base;
using FluentResults;
using HttpServices.AttachmentManager;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using Resources;
using ViewModels.Marketplace;

namespace RestFullApi.Controllers;

/// <summary>
///     مدیریت فایل ها
/// </summary>
public class AttachmentController : BaseControllerApi
{
    #region DI Settings & Constructor

    public AttachmentController(
        IMapper mapper,
        HttpClient httpClient, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
        : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
    {
    }

    #endregion

    #region GET : /

    /// <summary>
    ///     دریافت فایل های مورد نیاز برای یک رکورد از یک زیر سیستم خاص
    /// </summary>
    /// <param name="subSystemName">نام زیر سیستم</param>
    /// <param name="relationId">شناسه مربوط به فایل</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] string subSystemName, [FromQuery] string relationId)
    {
        var result = new Result<List<AttachmentResponseViewModel>>();

        if (string.IsNullOrEmpty(subSystemName))
        {
            var errorMessage = string.Format
                (Messages.RequiredError, DataDictionary.SubSystem);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(relationId))
        {
            var errorMessage = string.Format
                (Messages.RequiredError, DataDictionary.Guid);

            result.WithError(errorMessage);
        }

        if (result.IsFailed) return FluentResult(result);

        var subSystem = await UnitOfWork
            .SubSystemLocalRepository.FindByNameAsync(subSystemName);

        if (subSystem is null)
        {
            var errorMessage = string.Format
                (Messages.RequestNotValid);

            result.WithError(errorMessage);

            return FluentResult(result);
        }

        var entities =
            await UnitOfWork
                .AttachmentRepository
                .FindBySubSystemNameAndRelationIdAsync(subSystem.Id, relationId);

        var attachment =
            Mapper.Map<PagedList<AttachmentResponseViewModel>>(entities);

        result.WithValue(attachment);

        return FluentResult(result);
    }

    #endregion GET : /

    #region POST : /

    /// <summary>
    ///     ایجاد
    /// </summary>
    /// <param name="model">مدل نهایی</param>
    /// <returns></returns>
    [HttpPost]
    // [ServiceFilter<AttachmentViewModelFilterAction>]
    public async Task<IActionResult> CreateAsync([FromForm] AttachmentRequestViewModel model)
    {
        var result = new Result<AttachmentResponseViewModel>();

        var entity = Mapper.Map<Attachment>(model);

        var subSystem = await UnitOfWork
            .SubSystemLocalRepository.FindAsync(entity.SubSystemLocalId);

        if (subSystem is null) throw new NullReferenceException(nameof(subSystem));

        if (model.FileUpload is not null)
        {
            var service =
                new AttachmentService();

            var resultSaveFile = await service.UploadAsync
                (model.FileUpload, ServerKeyConstant.Key, subSystem.NameEN);

            if (resultSaveFile.IsFailed) result.WithErrors(resultSaveFile.Errors);

            if (resultSaveFile.IsSuccess)
            {
                var uploadAttachment = resultSaveFile.Value;

                if (uploadAttachment is not null)
                {
                    entity.FileName = uploadAttachment.FileName;
                    entity.FileOriginalName = uploadAttachment.FileOriginalName;
                    entity.FileThumbnailName = uploadAttachment.FileThumbnailName;
                }
            }
        }

        if (result.IsSuccess != true) return FluentResult(result);

        await UnitOfWork.AttachmentRepository.AddAsync(entity);
        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.CreateSuccessMessage,
            DataDictionary.Attachment);

        var attachment =
            Mapper.Map<AttachmentResponseViewModel>(entity);

        result.WithValue(attachment);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion POST : /

    #region DELETE : /

    /// <summary>
    ///     حذف با شناسه آن
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
    [HttpDelete("{id}")]
    [ServiceFilter<CheckAttachmentIdActionFilter>]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        var result = new Result<string>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Attachment;

        if (entity == null) throw new ArgumentNullException(nameof(entity));

        if (result.IsSuccess)
        {
            await UnitOfWork.AttachmentRepository.RemoveAsync(entity);
            await UnitOfWork.SaveAsync();

            if (entity.FileIsExist())
            {
                var service =
                    new AttachmentService();

                await service.DeleteAsync
                    (entity.FileName!, ServerKeyConstant.Key);
            }

            var successMessage =
                string.Format(Messages.DeleteMessageSuccess, DataDictionary.File);

            result.WithSuccess(successMessage);

            result.WithValue(entity.Id);
        }

        return FluentResult(result);
    }

    #endregion /DELETE : /
}