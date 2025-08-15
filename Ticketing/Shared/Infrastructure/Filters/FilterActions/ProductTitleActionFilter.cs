// using SampleResult;
// using ViewModels.Marketplace;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
//
// namespace Infrastructure.Filters.FilterActions;
//
// public class ProductTitleActionFilter :InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
// {
//     
//     public ProductTitleActionFilter()
//     {
//     }
//     
//     public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//     {
//         var result = new FluentResults.Result();
//         
//         var model =
//             context.ActionArguments.FirstOrDefault
//             (current =>
//                 current.Value is ProductTitleResponseViewModel).Value as ProductTitleResponseViewModel;
//
//         if (model is null)
//         {
//             result.WithError(Resources.Messages.RequestNotValid);
//         }
//         else
//         {
//             if (string.IsNullOrWhiteSpace(model.CategoryId))
//             {
//                 var errorMessage = string.Format(
//                     Resources.Messages.CategoryNameRequired, Resources.DataDictionary.Category);
//
//                 result.WithError(errorMessage);
//             }
//             if (string.IsNullOrWhiteSpace(model.NameEN))
//             {
//                 var errorMessage = string.Format(
//                     Resources.Messages.NameRequired, Resources.DataDictionary.NameEN);
//                 result.WithError(errorMessage);
//             }  
//             if (string.IsNullOrWhiteSpace(model.NameFA))
//             {
//                 var errorMessage = string.Format(
//                     Resources.Messages.NameRequired, Resources.DataDictionary.NameFA);
//                 result.WithError(errorMessage);
//             }  
//             
//             context.HttpContext.Items[Constants.ProjectKeyName.ObjectKey] = model;
//         }
//
//         if (result.IsSuccess == true)
//         {
//             await next();
//         }
//
//         if (result.IsFailed == true)
//         {
//             var sampleResult = result.ConvertToSampleResult();
//
//             context.Result =
//                 new BadRequestObjectResult(sampleResult);
//         }
//     }
// }