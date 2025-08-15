// using SampleResult;
// using RequestFeatures;
// using RazorPages.PageModals;
// using ViewModels.Marketplace;
// using HttpServices.Marketplace;
// using Microsoft.AspNetCore.Http;
// using Infrastructure.Marketplace;
// using ViewModels.ModelParameters;
// using BlazorComponents.Shared.Modal;
// using Infrastructure.Marketplace.Base;
// using Microsoft.AspNetCore.Components;
// using BaseProject.Model.ViewModel.Public;
//
// namespace RazorPages;
//
// public partial class Product
//     : ComponentBaseMarketplaceFullCustom<ProductResponseViewModel, ProductRequestViewModel>
// {
// 	[Inject] ProductService Service { get; set; } = default!;                          
// 	
//     private ModalImageProductChanger? _modalImageProductChanger;
//     
//     private ModalQuestionDeleteAttachments? _modalUpdateQuestionDeleteAttachments;
//     private ModalUpdateQuestion? _modalUpdateAndMoveAllProductToRequestForCheckPriceQuestion;
//     
//     private ProductParameters _parameters;
//     
//     public Product() : base()
//     {
//         _parameters = new ProductParameters();
//
//         #region PageDescriptionModel Initialize
//         _pageDescription = new PageDescriptionModel(
//             PageTitle: " مدیریت محصولات",
//             null,
//             [
//                 "با استفاده از ترتیب، میتوانیم ترتیب ها را در نرم افزار و سامانه مدیریت کنیم",
//                 "در صورتی که یک مورد از رکورد های این صفحه شبکه اجتماعی میباشد باید به جای محتوای صفحه لینک قرار دهید",
//                 "همچنین برای شبکه های مجازی تصاویر اجباری است و بهتر است قرار بگیرد",
//                 "--------------------------------------------------------------------------------",
//                 "صفحات مربوط به فوترها با تعدادی از اعداد بخش بندی میشوند که برای Order آن ها ست میشود",
//                 "عدد 5 شعار دکویاب",
//                 "عدد 10 با دکویاب",
//                 "عدد 20 خدمات دکویاب",
//                 "عدد 30 راهنمای خرید",
//                 "عدد 40 خدمات مشتریان",
//                 "عدد 50 فعلا جهت تست",
//                 "عدد 70 جهت کنترل و مدیریت بخش کلاب طراحی شده است",
//                 "برای تنظیم توضیحات هر دسته بندی نیز میتوانید از طریق همین صفحه اقدام کنید",
//                 "عدد 100 مربوط به متن آخر فوتر میباشد که مربوط به قوانین کپی رایت سامانه میباشد",
//             ],
//             new Dictionary<string, string>
//             {
//                 { "/Products/VisualizerAttachment", "تصاویر مربوط به مکان ها" },
//                 { "/Products/Products/List", "محصولات" },
//                 { "/Products/Categories", "دسته بندی محصولات" },
//                 { "/Products/Products/Add", "ثبت محصولات" },
//             }
//         );
//
//         #endregion /PageDescriptionModel Initialize
//     }
//
//     protected override async Task OnInitializedAsync()
//     {
//         _parameters =
//             UrlQueryService.GetRequestParameters(_parameters)!;
// 		
//         await SetListAsync();
//
//         if (IsEditMode == true)
//         {
//             await FindByIdAndMoveToUpdateModeAsync(Id!);
//         }
//     }
//
//     protected override async Task OnParametersSetAsync()
//     {
//         if (IsEditMode == true)
//         {
//             await FindByIdAndMoveToUpdateModeAsync(Id!);
//         }
//         else
//         {
//             OffUpdateMode();
//         }
//     }
//     
//     private async Task FindByIdAndMoveToUpdateModeAsync(string id)
//     {
//         var result = await Service.GetByIdAsync(id);
//
//         if (result is { IsSuccess: true, Value: not null })
//         {
//             OnUpdateMode(result.Value);
//         }
//         else
//         {
//             RequestResultService.AddResult(result);
//             await GoToPageAsync(MarketplaceRoutes.Product(), null);
//         }
//     }
//
//     private void OnFileHandle(List<IFormFile>? list)
//     {
//         var result = new FluentResults.Result();
//
//         if (list is not null && list.Any() == true && list[0].Length != 0)
//         {
//             Model.FileUpload = list[0];
//         }
//         else
//         {
//             Model.FileUpload = null;
//             result.WithError(Resources.Messages.ErroZiroByteFile);
//         }
//
//         RequestResultService.AddResult(result.ConvertToSampleResult());
//     }
//
//     private void BranchOnChanged(string id)
//     {
//         Model.BranchIds.Clear();
//         
//         if (string.IsNullOrEmpty(id) == false)
//         {
//             Model.BranchIds.Add(id);
//         }
//     }
//     
//     #region Send Request To Backend
//
//     private async Task SetListAsync()
//     {
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.ShowAsync();
//         }
//
//         var result = await Service.GetAsync(_parameters!);
//
//         RequestResultService.AddResult(result);
//
//         if (result.IsSuccess == true)
//         {
//             List = result.Value?.Data;
//             _metaData = result.Value?.MetaData;
//         }
//
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.HideAsync();
//         }
//     }
//
//     private async Task CreateAsync()
//     {
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.ShowAsync();
//         }
//
//         var result = Model.Validate();
//
//         RequestResultService.AddResult(result);
//
//         if (result.IsSuccess == true)
//         {
//             var createResult =
//                 await Service.CreateAsync(Model);
//
//             RequestResultService.AddResult(createResult);
//
//             if (createResult is { IsSuccess: true, Value: not null })
//             {
//                 InsertRecordInTable(createResult.Value);
//             }
//         }
//         else
//         {
//             RequestResultService.AddResult(result);
//         }
//
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.HideAsync();
//         }
//     }
//
//     private async Task UpdateAsync()
//     {
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.ShowAsync();
//         }
//
//         if (string.IsNullOrEmpty(Model.Id) == false)
//         {
//             Result result = Model.Validate();
//
//             RequestResultService.AddResult(result);
//
//             if (result.IsSuccess == true)
//             {
//                 var updateResult = await Service.UpdateAsync(Model);
//                 RequestResultService.AddResult(updateResult);
//
//                 if (updateResult is { IsSuccess: true, Value: not null })
//                 {
//                     UpdateRecordInTable(updateResult.Value);
//
//                     OffUpdateMode();
//                 }
//             }
//             else
//             {
//                 RequestResultService.AddResult(result);
//             }
//         }
//         else
//         {
//             FluentResults.Result result = new FluentResults.Result();
//             result.WithError(Resources.Messages.EditError);
//
//             RequestResultService.AddResult(result.ConvertToSampleResult());
//
//             OffUpdateMode();
//         }
//
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.HideAsync();
//         }
//     }
//     
//     private async Task DeleteAsync(string id)
//     {
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.ShowAsync();
//         }
//
//         var result =
//             await Service.DeleteAsync(id);
//
//         RequestResultService.AddResult(result);
//
//         if (result is { IsSuccess: true, Value: not null })
//         {
//             RemoveRecordInTable(result.Value);
//         }
//
//         if (_loadingModal is not null)
//         {
//             await _loadingModal.HideAsync();
//         }
//     }
//
//     private async Task CallbackMetaData(MetaData metaData)
//     {
//         _parameters.PageSize = metaData.PageSize;
//         _parameters.PageNumber = metaData.CurrentPage;
//
//         OffUpdateMode();
//         
//         await SetListAsync();
//     }
//     
//     private async Task OnSearchAsync(string textSearch)
//     {
//         _parameters.Text = textSearch;
//         await _paginationSystem.FirstPageAsync();
//     }
//
//     #endregion /Send Request To Backend
//
// }