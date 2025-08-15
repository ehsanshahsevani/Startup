using BaseProject.Model.ViewModel.Public;
using BlazorComponents.Services;
using BlazorComponents.Shared;
using BlazorComponents.Shared.Modal;
using HttpServices.Marketplace;
using Microsoft.AspNetCore.Components;
using SampleResult;
using ViewModels.Marketplace;

namespace RazorPages;

public partial class TalaSootSettings : ComponentBase
{
	[Inject] public TalaSootSettingsService? TalaSootSettingsService { get; set; }
	[Inject] public RequestResultService? RequestResultService { get; set; }

	private TalaSootSettingsResponseViewModel? TalaSootSettingsResponseViewModel { get; set; }

	private TalaSootSettingsRequestViewModel? TalaSootSettingsRequestViewModel { get; set; }

	private ModalLoading? _loadingModal;
	private PageDescriptionModel _pageDescription;

	private ModalUpdateQuestion? _modalUpdateQuestion;

	private string? _message;

	public TalaSootSettings()
	{
		_message = Resources.Messages.Loading;

		#region PageDescriptionModel Initialize

		_pageDescription = new PageDescriptionModel(
			PageTitle: "تنظیمات طلاسوت",
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

	protected override async Task OnInitializedAsync()
	{
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender == true)
		{
			await _loadingModal!.ShowAsync();

			var result =
				await TalaSootSettingsService!.GetSettingsAsync();

			if (result.IsSuccess == true)
			{
				TalaSootSettingsResponseViewModel = result.Value;

				TalaSootSettingsRequestViewModel =
					TalaSootSettingsResponseViewModel!.ToRequest();
			
				StateHasChanged();
			}
			else
			{
				_message = Resources.Messages.ServerReqestError;
			}

			await _loadingModal!.HideAsync();
		}
	}

	private async Task UpdateAsync()
	{
		if (_loadingModal is not null)
		{
			await _loadingModal.ShowAsync();
		}

		if (string.IsNullOrEmpty(TalaSootSettingsRequestViewModel!.Id) == false)
		{
			Result result = TalaSootSettingsRequestViewModel.Validate();

			RequestResultService!.AddResult(result);

			if (result.IsSuccess == true)
			{
				var updateResult = await TalaSootSettingsService!
					.UpdateAsync(TalaSootSettingsRequestViewModel);
				
				RequestResultService.AddResult(updateResult);

				if (result.IsSuccess == true)
				{
					TalaSootSettingsResponseViewModel = updateResult.Value;
					
					TalaSootSettingsRequestViewModel =
						TalaSootSettingsResponseViewModel!.ToRequest();
				}
			}
			else
			{
				RequestResultService.AddResult(result);
			}
		}
		else
		{
			FluentResults.Result result = new FluentResults.Result();
			result.WithError(Resources.Messages.EditError);

			RequestResultService!.AddResult(result.ConvertToSampleResult());
		}

		if (_loadingModal is not null)
		{
			await _loadingModal.HideAsync();
		}
	}
}