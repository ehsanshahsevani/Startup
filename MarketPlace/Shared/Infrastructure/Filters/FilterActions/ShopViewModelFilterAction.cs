using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using SampleResult;
using ViewModels.Marketplace;

namespace Infrastructure.Filters.FilterActions;

public class ShopViewModelFilterAction :
    InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public IUnitOfWork UnitOfWork { get; }

    public ShopViewModelFilterAction(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new FluentResults.Result();

        object? action =
            context.RouteData.Values[Constants.ProjectKeyName.ActionKey];

        object? controller
            = context.RouteData.Values[Constants.ProjectKeyName.ControllerKey];

        // **************************************************
        bool tryGetValue =
            context.ActionArguments.TryGetValue("model", out object? modelObject);
        // **************************************************

        if (tryGetValue == true && modelObject is ShopRequestViewModel model)
        {
            var resultModel = model.Validate();

            result.WithErrors(resultModel.Errors);
            
            if (result.IsSuccess == true)
            {
                // 0.5. check for repeat data in the database
                var checkForAddResult =
                    await UnitOfWork.ShopRepository.IsOkForAddAsync(model);

                result.WithErrors(checkForAddResult.Errors);
                
                // 1. check id
                // 2. get entity from the database if exist
                // 3. save in context
                // 4. continue to action
                //////////////////////////////////////////////
                if (string.IsNullOrEmpty(model.Id) == false)
                {
                    Shop? entity = 
                        await UnitOfWork.ShopRepository.FindAsync(model.Id);

                    if (entity is null)
                    {
                        var errorMessage = 
                            string.Format(Resources.Messages.NotFoundError, Resources.DataDictionary.Shop);
                        
                        result.WithError(errorMessage);
                    }
                    else
                    {
                        context.HttpContext.Items[Constants.ProjectKeyName.ObjectKey] = entity;
                    }
                }
            }
        }
        else
        {
            result.WithError(Resources.Messages.RequestNotValid);
        }

        if (result.IsSuccess)
        {
            await next();
        }
        else
        {
            var sampleResult = result.ConvertToSampleResult();

            context.Result =
                new BadRequestObjectResult(sampleResult);
        }
    }
}