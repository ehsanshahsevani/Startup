using Resources;
using AutoMapper;
using Persistence;
using Microsoft.AspNetCore.Mvc;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using PersistenceSeedworks.LogManager;
using System.ComponentModel.DataAnnotations;
using SampleResult;

namespace RestFullApi.Controllers;

/// <summary>
/// مدریت کارمزدهای سیستمی
/// </summary>
public class IncomeManagerController: Infrastructure.BaseControllerApi
{
	#region DI Settings & Constructor

	public IncomeManagerController(
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
	/// دریافت کارمزدها از بخش های مختلف
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetBoxDataAsync()
	{
		var result =
			new FluentResults.Result<IncomeBoxResponseViewModel>();

		var value = new IncomeBoxResponseViewModel
		{
			CommissionFeeAmount =
				await UnitOfWork.IncomeCommissionFeeRepository.FindIncomeAmountAsync(),
			
			SelleOfGoldFeeAmount =
				await UnitOfWork.IncomeSaleOfGoldFeeRepository.FindIncomeAmountAsync(),
			
			PurchaseGoldFeeAmount =
				await UnitOfWork.IncomeGoldPurchaseFeeRepository.FindIncomeAmountAsync(),
			
			RechargeWalletFeeAmount =
				await UnitOfWork.IncomeWalletRechargeFeeRepository.FindIncomeAmountAsync(),
			
			MaintenanceAndInsuranceFeeAmount =
				await UnitOfWork.IncomeMaintenanceAndInsuranceFeeRepository.FindIncomeAmountAsync()
		};
		
		result.WithValue(value);
		
		return FluentResult(result);
	}

	#endregion /GET : box-data
	
	#region PUT : update-box-data
	/// <summary>
	/// بروز رسانی کارمزدها از بخش های مختلف
	/// </summary>
	/// <returns></returns>
	[HttpPut]
	public async Task<IActionResult> UpdateBoxDataAsync(
		[FromBody] IncomeBoxRequestViewModel request)
	{
		var result =
			new FluentResults.Result<IncomeBoxResponseViewModel>();

		if (request.Validate().IsSuccess == false)
		{
			result.WithErrors(request.Validate().Errors);
		}

		if (result.IsSuccess == true)
		{
			var commissionFeeAmount =
				await UnitOfWork.IncomeCommissionFeeRepository.FindIncomeAsync();
			
			var selleOfGoldFeeAmount =
				await UnitOfWork.IncomeSaleOfGoldFeeRepository.FindIncomeAsync();
			
			var purchaseGoldFeeAmount =
				await UnitOfWork.IncomeGoldPurchaseFeeRepository.FindIncomeAsync();
			
			var rechargeWalletFeeAmount =
				await UnitOfWork.IncomeWalletRechargeFeeRepository.FindIncomeAsync();
			
			var maintenanceAndInsuranceFeeAmount =
				await UnitOfWork.IncomeMaintenanceAndInsuranceFeeRepository.FindIncomeAsync();
			
			commissionFeeAmount!.Amount = request.CommissionFeeAmount;
			selleOfGoldFeeAmount!.Amount = request.SelleOfGoldFeeAmount;
			purchaseGoldFeeAmount!.Amount = request.PurchaseGoldFeeAmount;
			rechargeWalletFeeAmount!.Amount = request.RechargeWalletFeeAmount;
			maintenanceAndInsuranceFeeAmount!.Amount = request.MaintenanceAndInsuranceFeeAmount;
			
			await UnitOfWork.SaveAsync();
			
			var value = new IncomeBoxResponseViewModel
			{
				CommissionFeeAmount =
					commissionFeeAmount.Amount,
			
				SelleOfGoldFeeAmount =
					selleOfGoldFeeAmount.Amount,
			
				PurchaseGoldFeeAmount =
					purchaseGoldFeeAmount.Amount,
			
				RechargeWalletFeeAmount =
					rechargeWalletFeeAmount.Amount,
			
				MaintenanceAndInsuranceFeeAmount =
					maintenanceAndInsuranceFeeAmount.Amount
			};
		
			result.WithValue(value);

			var successMessage = string.Format(
				Resources.Messages.UpdateMessageSuccess,
				Resources.DataDictionary.IncomiesSystem);

			result.WithSuccess(successMessage);
		}
		
		return FluentResult(result);
	}
	#endregion /PUT : update-box-data
}