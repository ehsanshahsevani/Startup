using Domain;
using Constants;
using Resources;
using AutoMapper;
using Domain.Base;
using Persistence;
using FluentResults;
using Infrastructure;
using RequestFeatures;
using Enums.Marketplace;
using ViewModels.Shared;
using ViewModels.Marketplace;
using Microsoft.AspNetCore.Mvc;
using ViewModels.ModelParameters;
using HttpServices.AttachmentManager;
using PersistenceSeedworks.LogManager;
using BaseProject.Model.ViewModel.Public;
using Infrastructure.Filters.FilterActions;

namespace RestFullApi.Controllers;

public class ProductController : BaseControllerApi
{
	#region DI Settings & Constructor

	public ProductController(
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
	///     مدل خروجی با دو کلید با نام های
	///     - data, metaData
	///     ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync([FromQuery] ProductParameters parameters)
	{
		var result = new Result<PagedListResult<ProductResponseViewModel>>();

		var entities =
			await UnitOfWork.ProductRepository.GetAllInPageAsync(parameters);

		var values =
			Mapper.Map<PagedList<ProductResponseViewModel>>(entities);

		await FetchAndSetAttachmentAsync
			<ProductResponseViewModel, ProductRequestViewModel>(nameof(Product), values);

		result.WithValue(new PagedListResult<ProductResponseViewModel>(values, entities.MetaData));

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
	[ServiceFilter<CheckProductIdActionFilter>]
	public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
	{
		var result = new Result<ProductResponseViewModel>();

		var entity =
			await UnitOfWork.ProductRepository.FindWithJoinAsync(id);

		var value =
			Mapper.Map<ProductResponseViewModel>(entity);

		await FetchAndSetAttachmentAsync
			<ProductResponseViewModel, ProductRequestViewModel>(nameof(Product), [value]);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : by-id

	#region GET : /dropdown-data

	/// <summary>
	///     دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
	///     - dropdown, ect
	/// </summary>
	/// <returns></returns>
	[HttpGet("dropdown-data")]
	public async Task<IActionResult> GetDropDownDataAsync()
	{
		var result = new Result<List<UiSelectModel>>();

		var value =
			await UnitOfWork.ProductRepository.GetSelectValues();

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /dropdown-data

	#region POST : /

	/// <summary>
	///     ایجاد
	/// </summary>
	/// <param name="model">مدل با ویژگی های لازم</param>
	/// <returns></returns>
	[HttpPost]
	[ServiceFilter<ProductViewModelFilterAction>]
	public async Task<IActionResult> CreateAsync([FromForm] ProductRequestViewModel model)
	{
		var result =
			new Result<ProductResponseViewModel>();

		var entity = Mapper.Map<Product>(model);

		entity.ProductBranches.Add(new ProductBranch
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			ProductId = entity.Id,
			BranchId = model.BranchIds.First()
		});

		await UnitOfWork.ProductRepository.AddAsync(entity);

		if (model.FileUpload != null)
		{
			var resultUploadAttachment = await UploadAndCreateAttachmentByFormFileAsync
				(model.FileUpload, AttachmentSubjectEnum.ProductProfile, nameof(Product), entity.Id);

			result.WithErrors(resultUploadAttachment.Errors);

			if (resultUploadAttachment.IsSuccess)
			{
				await UnitOfWork.AttachmentRepository
					.AddAsync(resultUploadAttachment.Value);
			}
		}

		if (result.IsSuccess)
		{
			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				Messages.CreateSuccessMessage,
				DataDictionary.Product);

			result.WithSuccess(successMessage);

			var entityId = entity.Id;

			entity = await UnitOfWork.ProductRepository.FindAsync(entityId);

			var value =
				Mapper.Map<ProductResponseViewModel>(entity);

			await FetchAndSetAttachmentAsync
				<ProductResponseViewModel, ProductRequestViewModel>(nameof(Product), [value]);

			result.WithValue(value);
		}

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
	[ServiceFilter<ProductViewModelFilterAction>]
	public async Task<IActionResult> UpdateAsync([FromForm] ProductRequestViewModel model)
	{
		var result = new Result<ProductResponseViewModel>();

		var entity = HttpContext.Items[ProjectKeyName.ObjectKey] as Product;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		Mapper.Map(model, entity);

		await UnitOfWork.ProductBranchRepository.RemoveByProductIdAsync(entity.Id);

		var newProductBranch = new ProductBranch
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			BranchId = model.BranchIds.First(),
			ProductId = entity.Id
		};

		await UnitOfWork.ProductBranchRepository.AddAsync(newProductBranch);

		await UnitOfWork.SaveAsync();

		var value =
			Mapper.Map<ProductResponseViewModel>(entity);

		await FetchAndSetAttachmentAsync
			<ProductResponseViewModel, ProductRequestViewModel>(nameof(Product), [value]);

		result.WithValue(value);

		var successMessage = string.Format(Messages.UpdateMessageSuccess, DataDictionary.Product);

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
	[ServiceFilter<CheckProductIdActionFilter>]
	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
	{
		var result = new Result<ProductResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Product;

		if (entity == null) throw new ArgumentNullException(nameof(entity));

		entity.IsActive = !entity.IsActive;
		entity.UpdateDateTime = DateTime.Now;


		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Messages.UpdateMessageSuccess, DataDictionary.Product);

		result.WithSuccess(successMessage);

		var value = Mapper.Map<ProductResponseViewModel>(entity);

		await FetchAndSetAttachmentAsync
			<ProductResponseViewModel, ProductRequestViewModel>(nameof(Product), [value]);

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
	[ServiceFilter<CheckProductIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Product;

		if (entity is null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		await UnitOfWork.ProductBranchRepository.RemoveByProductIdAsync(entity.Id);
		
		await UnitOfWork.ProductRepository.RemoveAsync(entity);
		
		var deleteImageResult = await RemoveAttachmentAsync(nameof(Product), id);

		if (deleteImageResult.IsSuccess == true)
		{
			await UnitOfWork.SaveAsync();

			var successMessage =
				string.Format(Messages.DeleteMessageSuccess, DataDictionary.Product);

			result.WithSuccess(successMessage);

			result.WithValue(entity.Id);
		}

		result.WithErrors(deleteImageResult.Errors);
		return FluentResult(result);
	}

	#endregion /DELETE : /

	#region PUT : update-profile/{id}

	/// <summary>
	///     تغییر تصویر
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <param name="file">فایل جدید</param>
	/// <returns>مدل حاوی تصویر جدید</returns>
	[HttpPut("update-profile/{id}")]
	[ServiceFilter<CheckProductIdActionFilter>]
	public async Task<IActionResult> UpdateProfileAsync(string id, IFormFile? file)
	{
		var result = new Result<ProductResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Product;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		if (file is not null)
		{
			var imageResult = await UploadAndCreateAttachmentByFormFileAsync
				(file, AttachmentSubjectEnum.ProductProfile, nameof(Product), entity.Id);

			if (imageResult.IsSuccess)
			{
				await UnitOfWork.AttachmentRepository.AddAsync(imageResult.Value);
			}
			else
			{
				result.WithErrors(imageResult.Errors);
			}
		}
		else
		{
			var errorMessage = string.Format
				(Messages.RequiredError, DataDictionary.Image);

			result.WithError(errorMessage);

			return FluentResult(result);
		}

		var attachments =
			await UnitOfWork
				.AttachmentRepository
				.FindAllAttachmentsByIdsAndSubSystemName([id], nameof(Product));

		if (attachments.Any() == false)
		{
			// throw new NullReferenceException(nameof(attachments));
		}

		if (result.IsSuccess)
		{
			await UnitOfWork
				.AttachmentRepository.RemoveRangeAsync(attachments);

			await UnitOfWork.SaveAsync();

			foreach (var profile in attachments)
			{
				var service =
					new AttachmentService();

				var resultSaveFile = await service.DeleteAsync
					(profile.FileName!, ServerKeyConstant.Key);

				if (resultSaveFile.IsFailed)
				{
					result.WithErrors(resultSaveFile.Errors);
				}
			}

			var successMessage =
				string.Format(Messages.UpdateMessageSuccess, DataDictionary.Product);

			result.WithSuccess(successMessage);

			var value =
				Mapper.Map<ProductResponseViewModel>(entity);

			await FetchAndSetAttachmentAsync
				<ProductResponseViewModel, ProductRequestViewModel>(nameof(Product), [value]);

			result.WithValue(value);
		}

		return FluentResult(result);
	}

	#endregion /PUT : /update-profile/{id}
}