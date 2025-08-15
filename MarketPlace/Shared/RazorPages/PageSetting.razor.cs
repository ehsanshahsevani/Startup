using SampleResult;
using RequestFeatures;
using ViewModels.Marketplace;
using HttpServices.Marketplace;
using Microsoft.AspNetCore.Http;
using ViewModels.ModelParameters;
using BlazorComponents.Shared.Modal;
using Infrastructure.Marketplace.Base;
using Microsoft.AspNetCore.Components;
using BaseProject.Model.ViewModel.Public;

namespace RazorPages;

public partial class PageSetting
    : ComponentBaseMarketplaceFullCustom<PageSettingResponseViewModel, PageSettingRequestViewModel>
{
	[Inject] PageSettingService PageSettingService { get; set; } = default!;                    
    
    private PageSettingParameters _parameters;
    private ModalQuestionDeleteAttachments? _modalUpdateQuestionDeleteAttachments;

    public PageSetting() : base()
    {
        
        _parameters = new PageSettingParameters();

        #region PageDescriptionModel Initialize
        _pageDescription = new PageDescriptionModel(
            PageTitle: "تنظیمات صفحات",
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
        _parameters =
            UrlQueryService.GetRequestParameters(new PageSettingParameters())!;

        await SetListAsync();
    }

    private void OnFileHandleWeb(List<IFormFile>? list)
    {
        var result = new FluentResults.Result();

        if (list is not null && list.Any() == true && list[0].Length != 0)
        {
            Model.FileWeb = list[0];
        }
        else
        {
            Model.FileWeb = null;
            result.WithError(Resources.Messages.ErroZiroByteFile);
        }

        RequestResultService.AddResult(result.ConvertToSampleResult());
    }
    
    private void OnFileHandleMobile(List<IFormFile>? list)
    {
        var result = new FluentResults.Result();

        if (list is not null && list.Any() == true && list[0].Length != 0)
        {
            Model.FileWeb = list[0];
        }
        else
        {
            Model.FileWeb = null;
            result.WithError(Resources.Messages.ErroZiroByteFile);
        }

        RequestResultService.AddResult(result.ConvertToSampleResult());
    }

    #region Send Request To Backend

    private async Task SetListAsync()
    {
        OffUpdateMode();

        if (_loadingModal is not null)
        {
            await _loadingModal.ShowAsync();
        }

        var result = await PageSettingService.GetAsync(_parameters!);

        RequestResultService.AddResult(result);

        if (result.IsSuccess == true)
        {
            List = result.Value?.Data;
            _metaData = result.Value?.MetaData;
        }

        if (_loadingModal is not null)
        {
            await _loadingModal.HideAsync();
        }
    }

    private async Task CreateAsync()
    {
        if (_loadingModal is not null)
        {
            await _loadingModal.ShowAsync();
        }

        var result = Model.Validate();

        RequestResultService.AddResult(result);

        if (result.IsSuccess == true)
        {
            var createResult =
                await PageSettingService.CreateAsync(Model);

            RequestResultService.AddResult(createResult);

            if (createResult is { IsSuccess: true, Value: not null })
            {
                InsertRecordInTable(createResult.Value);
            }
        }
        else
        {
            RequestResultService.AddResult(result);
        }

        if (_loadingModal is not null)
        {
            await _loadingModal.HideAsync();
        }
    }

    private async Task UpdateAsync()
    {
        if (_loadingModal is not null)
        {
            await _loadingModal.ShowAsync();
        }

        if (string.IsNullOrEmpty(Model.Id) == false)
        {
            Result result = Model.Validate();

            RequestResultService.AddResult(result);

            if (result.IsSuccess == true)
            {
                var updateResult = await PageSettingService.UpdateAsync(Model);
                RequestResultService.AddResult(updateResult);

                if (updateResult is { IsSuccess: true, Value: not null })
                {
                    UpdateRecordInTable(updateResult.Value);

                    OffUpdateMode();
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

            RequestResultService.AddResult(result.ConvertToSampleResult());

            OffUpdateMode();
        }

        if (_loadingModal is not null)
        {
            await _loadingModal.HideAsync();
        }
    }

    private async Task DeleteAsync(string id)
    {
        if (_loadingModal is not null)
        {
            await _loadingModal.ShowAsync();
        }

        var result =
            await PageSettingService.DeleteAsync(id);

        RequestResultService.AddResult(result);

        if (result is { IsSuccess: true, Value: not null })
        {
            RemoveRecordInTable(result.Value);
        }

        if (_loadingModal is not null)
        {
            await _loadingModal.HideAsync();
        }
    }
    private async Task CallbackMetaData(MetaData metaData)
    {
        _parameters.PageSize = metaData.PageSize;
        _parameters.PageNumber = metaData.CurrentPage;
        OffUpdateMode();
        await SetListAsync();
    }
    
    private async Task OnSearchAsync(string textSearch)
    {
        _parameters.Text = textSearch;
        await _paginationSystem.FirstPageAsync();
    }
    
    private async Task DeleteImageAsync(string id)
    {
        if (_loadingModal is not null)
        {
            await _loadingModal.ShowAsync();
        }

        var result = await PageSettingService.DeleteImageAsync(id);

        RequestResultService.AddResult(result);

        if (result is { IsSuccess: true, Value: not null })
        {
            UpdateRecordInTable(result.Value);
        }

        if (_loadingModal is not null)
        {
            await _loadingModal.HideAsync();
        }
    }
    
    #endregion /Send Request To Backend

    #region Page Structure
    
    private void UpdateModelTags(Dictionary<string, string> tags)
    {
        Model.Tags = tags;
    }

    #endregion /Page Structure
}