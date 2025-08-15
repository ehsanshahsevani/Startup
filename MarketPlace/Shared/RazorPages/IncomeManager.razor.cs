using HttpServices.Marketplace;
using Microsoft.AspNetCore.Components;
using BaseProject.Model.ViewModel.Public;
using BlazorComponents.Shared.Modal;
using Microsoft.AspNetCore.Identity;
using SampleResult;

namespace RazorPages;

public partial class IncomeManager :
	Infrastructure.Marketplace.Base.ComponentBaseMarketplaceWithModelCustom<IncomeBoxRequestViewModel>
{
	[Inject]
	public IncomeManagerService? ServiceIncome { get; set; }

	[Inject]
	public TreasuryManagerService? ServiceTreasury { get; set; }

	private ModalUpdateQuestion? _modalUpdateQuestionTreasury;
	
	private bool _hasLoaded = false;

	public TreasuryBoxRequestViewModel TreasuryBoxRequestViewModel { get; set; }
	
	public IncomeManager() : base()
	{
		#region PageDescriptionModel Initialize
		PageDescription = new PageDescriptionModel(
			PageTitle: "مدیریت مقادیر بخش مالی",
			null,
			[
				"با استفاده از ترتیب، میتوانیم ترتیب ها را در نرم افزار و سامانه مدیریت کنیم",
				"در صورتی که یک مورد از رکورد های این صفحه شبکه اجتماعی میباشد باید به جای محتوای صفحه لینک قرار دهید",
				"همچنین برای شبکه های مجازی تصاویر اجباری است و بهتر است قرار بگیرد",
				"--------------------------------------------------------------------------------",
				"صفحات مربوط به فوترها با تعدادی از اعداد بخش بندی میشوند که برای Order آن ها ست میشود",
				"عدد 5 شعار دکویاب",
				"عدد 10 با دکویاب",
				"عدد 20 خدمات دکویاب",
				"عدد 30 راهنمای خرید",
				"عدد 40 خدمات مشتریان",
				"عدد 50 فعلا جهت تست",
				"عدد 70 جهت کنترل و مدیریت بخش کلاب طراحی شده است",
				"برای تنظیم توضیحات هر دسته بندی نیز میتوانید از طریق همین صفحه اقدام کنید",
				"عدد 100 مربوط به متن آخر فوتر میباشد که مربوط به قوانین کپی رایت سامانه میباشد",
			],
			new Dictionary<string, string>
			{
				{ "/Products/VisualizerAttachment", "تصاویر مربوط به مکان ها" },
				{ "/Products/Products/List", "محصولات" },
				{ "/Products/Categories", "دسته بندی محصولات" },
				{ "/Products/Products/Add", "ثبت محصولات" },
			}
		);

		#endregion /PageDescriptionModel Initialize
	}
	
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender == true)
		{
			await LoadingModal!.ShowAsync();

			await FetchDataIncomeAsync();
			await FetchDataTreasuryAsync();

			_hasLoaded = true;
			
			StateHasChanged();
			
			await LoadingModal!.HideAsync();
		}
	}

	private async Task FetchDataIncomeAsync()
	{
		var result =
			await ServiceIncome!.GetBoxDataAsync();

		RequestResultService.AddResult(result);
		
		if (result.IsSuccess == true)
		{
			base.Model = result.Value!.ToRequest();
		}
		else
		{
			base.Model = null;
		}
	}

	private async Task UpdateIncomeAsync()
	{
		var result = new FluentResults.Result();
		
		var resultHelper =
			Utilities.ValidationHelper
				.GetValidationResults(base.Model);

		result.WithErrors(resultHelper.Select(x => x.ErrorMessage));

		RequestResultService.AddResult(result.ConvertToSampleResult());

		if (result.IsSuccess == true)
		{
			var resultUpdate =
				await ServiceIncome!.UpdateBoxDataAsync(base.Model);
			
			RequestResultService.AddResult(resultUpdate);
		}
	}
	
	private async Task FetchDataTreasuryAsync()
	{
		var result =
			await ServiceTreasury!.GetBoxDataAsync();

		RequestResultService.AddResult(result);
		
		if (result.IsSuccess == true)
		{
			TreasuryBoxRequestViewModel = result.Value!.ToRequest();
		}
		else
		{
			base.Model = null;
		}
	}

	private async Task UpdateTreasuryAsync()
	{
		var result = new FluentResults.Result();

		var resultHelper = TreasuryBoxRequestViewModel.Validate();

		result.WithErrors(resultHelper.Errors);
			
		RequestResultService.AddResult(result.ConvertToSampleResult());

		if (result.IsSuccess == true)
		{
			var resultUpdate =
				await ServiceTreasury!.UpdateBoxDataAsync(TreasuryBoxRequestViewModel);
			
			RequestResultService.AddResult(resultUpdate);
		}
	}
}