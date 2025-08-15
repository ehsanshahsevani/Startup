using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using SampleResult;
using ViewModels.Marketplace;

namespace Infrastructure.Filters.FilterActions;

public class CheckShopConfirmModelActionFilter: InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public IUnitOfWork UnitOfWork { get; }

    public CheckShopConfirmModelActionFilter(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new FluentResults.Result();

        var shopConfirmationViewModel =
            context.ActionArguments.FirstOrDefault
            (current =>
                current.Value is ShopConfirmationViewModel).Value as ShopConfirmationViewModel;
        string id="";
        if (shopConfirmationViewModel is not null)
        {
            id= shopConfirmationViewModel.Id;
        }
        if (string.IsNullOrWhiteSpace(id) || id == Guid.NewGuid().ToString())
        {
            var errorMessage = string.Format(
                Resources.Messages.NotFoundError, Resources.DataDictionary.Guid);
            
            result.WithError(errorMessage);
        }
        else
        {
            var shop =
                await UnitOfWork
                    .ShopRepository.FindAsync(id);

            if (shop is null)
            {
                var errorMessage = string.Format(
                    Resources.Messages.NotFoundError, Resources.DataDictionary.Shop);
                
                result.WithError(errorMessage);
            }
            else
            {
                context.HttpContext.Items[Constants.ProjectKeyName.ObjectKey] = shop;
            }
        }

        if (result.IsSuccess == true)
        {
            await next();
        }

        if (result.IsFailed == true)
        {
            var sampleResult = result.ConvertToSampleResult();

            context.Result =
                new BadRequestObjectResult(sampleResult);
        }
    }
}