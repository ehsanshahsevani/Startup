using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using SampleResult;

namespace Infrastructure.Filters.FilterActions;

public class CheckShopIdActionFilter: InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public IUnitOfWork UnitOfWork { get; }

    public CheckShopIdActionFilter(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new FluentResults.Result();

        var id =
            context.ActionArguments.FirstOrDefault
            (current =>
                current.Value is string).Value as string;
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