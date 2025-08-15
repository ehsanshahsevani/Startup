using AutoMapper;
using Constants;
using Domain;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using Resources;
using ViewModels.Shared;
using ViewModels.Ticketing;
using ViewModels.Ticketing.ModelParameters;

namespace RestFullApi.Controllers;

public class TicketMessageMessageController : BaseControllerApi
{
    #region DI Settings & Constructor

    public TicketMessageMessageController(
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
    public async Task<IActionResult> GetAsync([FromQuery] TicketMessageParameters parameters)
    {
        var result = new Result<PagedListResult<TicketMessageResponseViewModel>>();

        var entities =
            await UnitOfWork.TicketMessageRepository.GetAllInPageAsync(parameters);

        var values =
            Mapper.Map<PagedList<TicketMessageResponseViewModel>>(entities);

        result.WithValue(new PagedListResult<TicketMessageResponseViewModel>(values, entities.MetaData));

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
    [ServiceFilter<CheckTicketMessageIdActionFilter>]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var result = new Result<TicketMessageResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as TicketMessage;

        var value =
            Mapper.Map<TicketMessageResponseViewModel>(entity);

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
    [ServiceFilter<TicketMessageViewModelFilterAction>]
    public async Task<IActionResult> CreateAsync([FromForm] TicketMessageRequestViewModel model)
    {
        var result =
            new Result<TicketMessageResponseViewModel>();

        var entity = Mapper.Map<TicketMessage>(model);

        await UnitOfWork.TicketMessageRepository.AddAsync(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.CreateSuccessMessage,
            DataDictionary.TicketMessage);

        result.WithSuccess(successMessage);

        var entityId = entity.Id;

        entity = await UnitOfWork.TicketMessageRepository.FindAsync(entityId);

        var value =
            Mapper.Map<TicketMessageResponseViewModel>(entity);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion POST : /

    #region PUT : /change-activation/{id}

    /// <summary>
    ///     تغییر وضعیت
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>مدل با دیتای جدید</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut("change-activation/{id}")]
    [ServiceFilter<CheckTicketMessageIdActionFilter>]
    public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
    {
        var result = new Result<TicketMessageResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as TicketMessage;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        entity.IsActive = !entity.IsActive;
        entity.UpdateDateTime = DateTime.Now;

        var value = Mapper.Map<TicketMessageResponseViewModel>(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.TicketMessage);

        result.WithSuccess(successMessage);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /PUT : /change-activation/{id}

}

public class CheckTicketMessageIdActionFilter : IFilterMetadata
{
}