using Constants;
using InfrastructureSeedworks.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using Resources;
using SampleResult;
using Result = FluentResults.Result;

namespace Infrastructure.Filters.FilterActions;

public class CheckCartItemProductIdActionFilter: IBaseAsyncActionFilter
{
    public CheckCartItemProductIdActionFilter(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new Result();

        var productId =
            context.ActionArguments.FirstOrDefault
            (current =>
                current.Value is string).Value as string;

        if (string.IsNullOrWhiteSpace(productId) || productId == Guid.NewGuid().ToString())
        {
            var errorMessage = string.Format(
                Messages.NotFoundError, DataDictionary.Guid);

            result.WithError(errorMessage);
        }
        else
        {
            var entities =
                await UnitOfWork
                    .CartItemRepository.FindByProductIdAsync(productId);

            if (entities is null)
            {
                var errorMessage = string.Format(
                    Messages.NotFoundError, DataDictionary.CartItem);

                result.WithError(errorMessage);
            }
            else
            {
                context.HttpContext.Items[ProjectKeyName.ObjectKey] = entities;
            }
        }

        if (result.IsSuccess) await next();

        if (result.IsFailed)
        {
            var sampleResult = result.ConvertToSampleResult();

            context.Result =
                new BadRequestObjectResult(sampleResult);
        }
    }
}