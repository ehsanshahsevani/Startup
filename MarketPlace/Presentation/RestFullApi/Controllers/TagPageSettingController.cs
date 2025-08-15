using AutoMapper;
using Persistence;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت تگ های مربوط به بخش تنظیمات صفحات
/// </summary>
public class TagPageSettingController : Infrastructure.BaseControllerApi
{
	#region DI Settings & Constructor

	public TagPageSettingController(
		IMapper mapper,
		HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
		: base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}

	#endregion
	
	#region GET : box-data

	/// <summary>
	/// دریافت اطلاعات یک رکورد با شناسه آن
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(string id)
	{
		var result =
			new FluentResults.Result<TreasuryBoxResponseViewModel>();

		var value = new TreasuryBoxResponseViewModel
		{
			TreasuryGoldOnline = 
				Convert.ToInt32(await UnitOfWork.GoldTreasuryOnlineRepository.FindFirstAmountAsync()),
			
			TreasuryGoldReceive = 
				Convert.ToInt32(await UnitOfWork.GoldTreasuryReceiveRepository.FindFirstAmountAsync()),
			
			TalaSootBankAccountAmount = 
				Convert.ToInt32(await UnitOfWork.TalaSootBankAccountRepository.FindFirstAmountAsync()),
		};
		
		result.WithValue(value);
		
		return FluentResult(result);
	}

	#endregion /GET : box-data
	
	#region PUT : update-box-data
	/// <summary>
	/// ثبت رکورد جدید
	/// </summary>
	/// <returns></returns>
	[HttpPut]
	public async Task<IActionResult> UpdateBoxDataAsync(
		[FromBody] TreasuryBoxRequestViewModel request)
	{
		var result =
			new FluentResults.Result<TreasuryBoxResponseViewModel>();

		if (request.Validate().IsSuccess == false)
		{
			result.WithErrors(request.Validate().Errors);
		}

		if (result.IsSuccess == true)
		{
			var goldTreasuryOnline =
				await UnitOfWork.GoldTreasuryOnlineRepository.FindFirstRecordAsync();
			
			var goldTreasuryReceive =
				await UnitOfWork.GoldTreasuryReceiveRepository.FindFirstRecordAsync();
			
			var talaSootBankAccount =
				await UnitOfWork.TalaSootBankAccountRepository.FindFirstRecordAsync();
			
			goldTreasuryOnline!.Amount = Convert.ToDecimal(request.TreasuryGoldOnline);
			goldTreasuryReceive!.Amount = Convert.ToDecimal(request.TreasuryGoldReceive);
			talaSootBankAccount!.Amount = Convert.ToDecimal(request.TalaSootBankAccountAmount);
			
			await UnitOfWork.SaveAsync();
			
			var value = new TreasuryBoxResponseViewModel
			{
				TreasuryGoldOnline =
					Convert.ToInt32(goldTreasuryOnline.Amount),
			
				TreasuryGoldReceive =
					Convert.ToInt32(goldTreasuryReceive.Amount),
			
				TalaSootBankAccountAmount =
					Convert.ToInt32(talaSootBankAccount.Amount),
			};
		
			result.WithValue(value);

			var successMessage = string.Format(
				Resources.Messages.UpdateMessageSuccess,
				Resources.DataDictionary.TreasurySystemAmount);

			result.WithSuccess(successMessage);
		}
		
		return FluentResult(result);
	}
	#endregion /PUT : update-box-data
}