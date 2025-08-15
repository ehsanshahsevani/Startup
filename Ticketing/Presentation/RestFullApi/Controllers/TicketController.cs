using AutoMapper;
using Constants;
using Domain;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using Resources;
using ViewModels.Shared;
using ViewModels.Ticketing;
using ViewModels.Ticketing.ModelParameters;

namespace RestFullApi.Controllers;

public class TicketController : BaseControllerApi
{
    #region DI Settings & Constructor

    public TicketController(
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
    ///     دریافت لیست همه به صورت صفحه بندی شده
    ///     مدل خروجی با دو کلید با نام های
    ///     - data, metaData
    ///     ارسال میشوند
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] TicketParameters parameters)
    {
        var result = new Result<PagedListResult<TicketResponseViewModel>>();

        var entities =
            await UnitOfWork.TicketRepository.GetAllInPageAsync(parameters);

        var values =
            Mapper.Map<PagedList<TicketResponseViewModel>>(entities);

        result.WithValue(new PagedListResult<TicketResponseViewModel>(values, entities.MetaData));

        return FluentResult(result);
    }

    #endregion GET : /

    #region GET : {id}

    /// <summary>
    ///     دریافت با استفاده از شناسه آن
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ServiceFilter<CheckTicketIdActionFilter>]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var result = new Result<TicketResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Ticket;

        var value =
            Mapper.Map<TicketResponseViewModel>(entity);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : by-id

    #region POST : /

    /// <summary>
    ///     ایجاد
    /// </summary>
    /// <param name="model">مدل با ویژگی های لازم</param>
    /// <returns></returns>
    [HttpPost]
    [ServiceFilter<TicketViewModelFilterAction>]
    public async Task<IActionResult> CreateAsync([FromForm] TicketRequestViewModel model)
    {
        var result =
            new Result<TicketResponseViewModel>();

        var entity = Mapper.Map<Ticket>(model);
        
        entity.IsSeen = false;
        
        var underReviewStatus = await UnitOfWork.StatusRepository.FindUnderReviewAsync();
        
        entity.StatusId = underReviewStatus!.Id;
        
        await UnitOfWork.TicketRepository.AddAsync(entity);

        ///todo:
        /// در صورتی که نوع تیکت ثبت شده از نوع گزارش خطا باشد
        /// باید به تیم فنی ایمیل ارسال شود
        ///     

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.CreateSuccessMessage,
            DataDictionary.Ticket);

        result.WithSuccess(successMessage);

        var entityId = entity.Id;

        entity = await UnitOfWork.TicketRepository.FindAsync(entityId);

        var value =
            Mapper.Map<TicketResponseViewModel>(entity);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion POST : /

    #region PUT : /

    /// <summary>
    ///     ویرایش
    /// </summary>
    /// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
    /// <returns>مدل نهایی و جدید</returns>
    [HttpPut]
    [ServiceFilter<TicketViewModelFilterAction>]
    public async Task<IActionResult> UpdateAsync([FromForm] TicketRequestViewModel model)
    {
        var result = new Result<TicketResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Ticket;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Mapper.Map(model, entity);

        await UnitOfWork.SaveAsync();

        entity = await UnitOfWork
            .TicketRepository.FindAsync(entity.Id);

        var value =
            Mapper.Map<TicketResponseViewModel>(entity);

        result.WithValue(value);

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.Ticket);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion /PUT : /

    #region PUT : /change-Status/{id}

    /// <summary>
    ///     تغییر وضعیت
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>مدل با دیتای جدید</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut("change-status/{id}")]
    [ServiceFilter<CheckTicketIdActionFilter>]
    public async Task<IActionResult> ChangeStatusAsync([FromRoute] string id, string statusId)
    {
        var result = new Result<TicketResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Ticket;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var status = await UnitOfWork.StatusRepository.FindAsync(statusId);
        if (status != null)
        {
            // var nextStatus = await UnitOfWork.StatusRepository.FindNextByOrderingAsync(status.Ordering);
            // if (nextStatus is null)
            // {
            //         var errorMessage =
            //             string.Format(
            //                 Messages.NotFoundError,
            //                 DataDictionary.State);
            //
            //         result.WithError(errorMessage);
            // }
            entity.StatusId = status.Id;
            entity.IsSeen = false;
        }


        entity.UpdateDateTime = DateTime.Now;

        var value = Mapper.Map<TicketResponseViewModel>(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.Ticket);

        result.WithSuccess(successMessage);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /PUT : /change-status/{id}


    #region PUT : /change-activation/{id}

    /// <summary>
    ///     تغییر وضعیت
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>مدل با دیتای جدید</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut("change-activation/{id}")]
    [ServiceFilter<CheckTicketIdActionFilter>]
    public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
    {
        var result = new Result<TicketResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Ticket;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        entity.IsActive = !entity.IsActive;
        entity.UpdateDateTime = DateTime.Now;

        var value = Mapper.Map<TicketResponseViewModel>(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.Ticket);

        result.WithSuccess(successMessage);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /PUT : /change-activation/{id}

    #region DELETE : /

    /// <summary>
    ///     حذف با شناسه آن
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
    [HttpDelete("{id}")]
    [ServiceFilter<CheckTicketIdActionFilter>]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        var result = new Result<string>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Ticket;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await UnitOfWork.TicketRepository.RemoveAsync(entity);
        await UnitOfWork.SaveAsync();

        var successMessage =
            string.Format(Messages.DeleteMessageSuccess, DataDictionary.Ticket);

        result.WithSuccess(successMessage);

        result.WithValue(entity.Id);

        return FluentResult(result);
    }

    #endregion /DELETE : /
}