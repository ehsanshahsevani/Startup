using AutoMapper;
using Domain;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;
using ViewModels.Marketplace;
using ViewModels.ProjectManager;
using IUnitOfWork = Persistence.IUnitOfWork;

namespace RestFullApi.Controllers;

/// <summary>
/// زیر سیستم ها
/// </summary>
public class SubSystemLocalController : Infrastructure.BaseControllerApi
{
	public SubSystemLocalController(IMapper mapper, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, ILogDetailManager logDetailManager, ILogServerManager logServerManager) : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}

	#region GET : /

	/// <summary>
	/// بخش های مختلف پروژه
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync()
	{
		var result = new Result<List<SubSystemResponseViewModel>>();

		var entities = 
			await UnitOfWork.SubSystemLocalRepository.GetAllAsync();

		var value = Mapper.Map<List<SubSystemResponseViewModel>>(entities);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /

	#region GET : /description/{subsystemName}

	/// <summary>
	/// دریافت توضیحات مربوط به زیر سیستم مورد نظر
	/// </summary>
	/// <param name="subSystemName">نام زیر سیستم مورد نظر</param>
	/// <returns></returns>
	[HttpGet(template: "description/{subSystemName}")]
	public async Task<IActionResult> GetDescriptionAsync(string subSystemName)
	{
		var result = new Result<string?>();

		var description = 
			await UnitOfWork.SubSystemLocalRepository
				.FindDescriptionBySubSystemNameAsync(subSystemName);

		result.WithValue(description);

		return FluentResult(result);
	}

	#endregion /GET : /description/{subsystemName}
	
	#region PUT : /update-description

	/// <summary>
	/// ویرایش توضیحات اضافی یک زیر سیستم برای پنل ادمین
	/// در تمامی صفحات خوانده میشود
	/// هر صفحه توضیحات و نکات مربوط به خودش را دارد
	/// که ادمین های مختلف باید آنهارا در هر صفحه ببینند
	/// </summary>
	/// <param name="id">شناسه زیر سیستم</param>
	/// <param name="description">توضیحات</param>
	/// <returns></returns>
	[HttpPut(template: "update-description/{id}")]
	public async Task<IActionResult> UpdateDescriptionAsync(string id, [FromBody] string description)
	{
		var result = new Result<SubSystemLocalResponseViewModel>();

		if (string.IsNullOrEmpty(id) == true)
		{
			result.WithError(Resources.Messages.RequestNotValid);
			return FluentResult(result);
		}
		
		var entity = await UnitOfWork
			.SubSystemLocalRepository.UpdateDescriptionByIdAsync(id, description);

		if (entity is not null)
		{
			await UnitOfWork.SaveAsync();
			
			var successMessage =
				string.Format(Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.SubSystem);
			
			result.WithSuccess(successMessage);
			
			var value = Mapper.Map<SubSystemLocalResponseViewModel>(entity);
			
			result.WithValue(value);
		}
		else
		{
			var errorMessage =
				string.Format(Resources.Messages.NotFoundError, Resources.DataDictionary.SubSystem);
			
			result.WithError(errorMessage);
		}
		
		return FluentResult(result);
	}
	#endregion /PUT : /UpdateDescription
}