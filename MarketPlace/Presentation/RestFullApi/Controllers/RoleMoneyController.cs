using Domain;
using Constants;
using Resources;
using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Persistence;
using FluentResults;
using Infrastructure;
using RequestFeatures;
using ViewModels.Shared;
using ViewModels.Marketplace;
using Microsoft.AspNetCore.Mvc;
using ViewModels.ModelParameters;
using PersistenceSeedworks.LogManager;
using Infrastructure.Filters.FilterActions;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت قوانین مالی
/// </summary>
public class RoleMoneyController : BaseControllerApi
{
    #region DI Settings & Constructor

    public RoleMoneyController(
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
    public async Task<IActionResult> GetAsync([FromQuery] RoleMoneyParameters parameters)
    {
        var result = new Result<PagedListResult<RoleMoneyResponseViewModel>>();

        var entities =
            await UnitOfWork.RoleMoneyRepository.GetAllInPageAsync(parameters);

        var values =
            Mapper.Map<PagedList<RoleMoneyResponseViewModel>>(entities);

        result.WithValue(new PagedListResult<RoleMoneyResponseViewModel>(values, entities.MetaData));

        return FluentResult(result);
    }

    #endregion GET : /

    #region GET : {id}

    /// <summary>
    ///  دریافت با استفاده از شناسه آن
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ServiceFilter<CheckRoleMoneyIdActionFilter>]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var result = new Result<RoleMoneyResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as RoleMoney;

        var value =
            Mapper.Map<RoleMoneyResponseViewModel>(entity);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : by-id

    #region GET : /time-data

    /// <summary>
    /// لیست ساعت های مجاز برای ثبت قانون
    ///     - dropdown, ect
    /// </summary>
    /// <returns></returns>
    [HttpGet("time-data")]
    public IActionResult GetDropDownDataAsync()
    {
        var result = new Result<List<UiSelectModel>>();

        var value = new List<UiSelectModel>();
		
        for(int i = 0; i <= 23; i++)
        {
            string hour = i.ToString("00");
			
            value.Add(new UiSelectModel($"{hour}:00",$"{hour}:00"));
            value.Add(new UiSelectModel($"{hour}:30",$"{hour}:30"));
        }

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : /time-data
    
    #region POST : /

    /// <summary>
    ///     ایجاد
    /// </summary>
    /// <param name="model">مدل با ویژگی های لازم</param>
    /// <returns></returns>
    [HttpPost]
    [ServiceFilter<RoleMoneyViewModelFilterAction>]
    public async Task<IActionResult> CreateAsync([FromForm] RoleMoneyRequestViewModel model)
    {
        var result =
            new Result<RoleMoneyResponseViewModel>();

        var entity = Mapper.Map<RoleMoney>(model);

        await UnitOfWork.RoleMoneyRepository.AddAsync(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.CreateSuccessMessage,
            DataDictionary.RoleMoney);

        result.WithSuccess(successMessage);

        var entityId = entity.Id;

        entity =
            await UnitOfWork
                .RoleMoneyRepository.FindAsync(entityId);

        var value =
            Mapper.Map<RoleMoneyResponseViewModel>(entity);

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
    [ServiceFilter<RoleMoneyViewModelFilterAction>]
    public async Task<IActionResult> UpdateAsync([FromForm] RoleMoneyRequestViewModel model)
    {
        var result = new Result<RoleMoneyResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as RoleMoney;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Mapper.Map(model, entity);

        await UnitOfWork.SaveAsync();

        entity = await UnitOfWork
            .RoleMoneyRepository.FindAsync(entity.Id);
        
        var value =
            Mapper.Map<RoleMoneyResponseViewModel>(entity);

        result.WithValue(value);

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.RoleMoney);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion /PUT : /

    #region PUT : /change-activation/{id}

    /// <summary>
    ///     تغییر وضعیت
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns>مدل با دیتای جدید</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut("change-activation/{id}")]
    [ServiceFilter<CheckRoleMoneyIdActionFilter>]
    public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
    {
        var result = new Result<RoleMoneyResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as RoleMoney;

        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.IsActive = !entity.IsActive;
        entity.UpdateDateTime = DateTime.Now;

        var value = Mapper.Map<RoleMoneyResponseViewModel>(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Messages.UpdateMessageSuccess, DataDictionary.RoleMoney);

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
    [ServiceFilter<CheckRoleMoneyIdActionFilter>]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        var result = new Result<string>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as RoleMoney;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await UnitOfWork.RoleMoneyRepository.RemoveAsync(entity);
        await UnitOfWork.SaveAsync();

        var successMessage =
            string.Format(Messages.DeleteMessageSuccess, DataDictionary.RoleMoney);

        result.WithSuccess(successMessage);

        result.WithValue(entity.Id);

        return FluentResult(result);
    }

    #endregion /DELETE : /
}