using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Constants;
using Domain;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace RestFullApi.Controllers;

public class CategoryController : BaseControllerApi
{
    #region DI Settings & Constructor

    public CategoryController(
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
    /// دریافت لیست همه به صورت صفحه بندی شده
    /// مدل خروجی با دو کلید با نام های
    /// - data, metaData
    /// ارسال میشوند
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] CategoryParameters parameters)
    {
        var result = new Result<PagedListResult<CategoryResponseViewModel>>();

        var entities =
            await UnitOfWork.CategoryRepository.GetAllInPageAsync(parameters);

        var value =
            Mapper.Map<PagedList<CategoryResponseViewModel>>(entities);

        result.WithValue(new PagedListResult<CategoryResponseViewModel>(value, entities.MetaData));

        return FluentResult(result);
    }

    #endregion GET : /

    #region GET : {id}

    /// <summary>
    /// دریافت با استفاده از شناسه آن
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet(template: "{id}")]
    [ServiceFilter<CheckCategoryIdActionFilter>]
    public IActionResult GetByIdAsync([FromRoute] string id)
    {
        var result = new Result<CategoryRequestViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Category;

        var value = Mapper.Map<CategoryRequestViewModel>(entity);

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : by-id

    #region GET : /dropdown-data

    /// <summary>
    /// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
    /// - dropdown, ect
    /// </summary>
    /// <returns></returns>
    [HttpGet(template: "dropdown-data")]
    public async Task<IActionResult> GetDropDownDataAsync()
    {
        var result = new Result<List<UiSelectModel>>();

        var value =
            await UnitOfWork.CategoryRepository.GetSelectValues();

        result.WithValue(value);

        return FluentResult(result);
    }

    #endregion /GET : /dropdown-data

    #region POST : /

    /// <summary>
    /// ایجاد
    /// </summary>
    /// <param name="model">مدل با ویژگی های لازم</param>
    /// <returns></returns>
    [HttpPost]
    [ServiceFilter<CategoryViewModelFilterAction>]
    public async Task<IActionResult> CreateAsync([FromForm] CategoryRequestViewModel model)
    {
        var result = new Result<CategoryResponseViewModel>();
        var entity = Mapper.Map<Category>(model);

        await UnitOfWork.CategoryRepository.AddAsync(entity);

        await UnitOfWork.SaveAsync();

        var successMessage = string.Format(
            Resources.Messages.CreateSuccessMessage,
            Resources.DataDictionary.Category);

        var value =
            Mapper.Map<CategoryResponseViewModel>(entity);

        result.WithValue(value);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion POST : /

    #region PUT : /

    /// <summary>
    /// ویرایش
    /// </summary>
    /// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
    /// <returns>مدل نهایی و جدید</returns>
    [HttpPut]
    [ServiceFilter<CategoryViewModelFilterAction>]
    public async Task<IActionResult> UpdateAsync([FromForm] CategoryRequestViewModel model)
    {
        var result = new Result<CategoryResponseViewModel>();

        var entity =
            HttpContext.Items[ProjectKeyName.ObjectKey] as Category;

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        Mapper.Map(model, entity);

        await UnitOfWork.SaveAsync();
        
        var value =
            Mapper.Map<CategoryResponseViewModel>(entity);

        result.WithValue(value);

        var successMessage = string.Format(
            Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.Category);

        result.WithSuccess(successMessage);

        return FluentResult(result);
    }

    #endregion /PUT : /
    
	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل با دیتای جدید</returns>
	/// <exception cref="ArgumentNullException"></exception>
	[HttpPut(template: "change-activation/{id}")]
	[ServiceFilter<CheckCategoryIdActionFilter>]
	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
	{
		var result = new Result<CategoryResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Category;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		entity.IsActive = !entity.IsActive;
		entity.UpdateDateTime = DateTime.Now;

		var value = Mapper.Map<CategoryResponseViewModel>(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.Category);

		result.WithSuccess(successMessage);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /PUT : /change-activation/{id}

	#region DELETE : /

	/// <summary>
	/// حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
	[HttpDelete(template: "{id}")]
	[ServiceFilter<CheckCategoryIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		Category? entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Category;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}
		
		if (result.IsSuccess == true)
		{
			await UnitOfWork.CategoryRepository.RemoveAsync(entity);
			await UnitOfWork.SaveAsync();

			var successMessage =
				string.Format(Resources.Messages.DeleteMessageSuccess, Resources.DataDictionary.Category);

			result.WithSuccess(successMessage);

			result.WithValue(entity.Id);
		}

		return FluentResult(result);
	}

	#endregion /DELETE : /
}