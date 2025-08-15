using Constants;
using InfrastructureSeedworks.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using Resources;
using SampleResult;
using Result = FluentResults.Result;

namespace Infrastructure.Filters.FilterActions;

public class CheckTypeTypeRoleGoldIdActionFilter : IBaseAsyncActionFilter
{
    public CheckTypeTypeRoleGoldIdActionFilter(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new Result();

        var id =
            context.ActionArguments.FirstOrDefault
            (current =>
                current.Value is string).Value as string;

        if (string.IsNullOrWhiteSpace(id) || id == Guid.NewGuid().ToString())
        {
            var errorMessage = string.Format(
                Messages.NotFoundError, DataDictionary.Guid);

            result.WithError(errorMessage);
        }
        else
        {
            var entity =
                await UnitOfWork
                    .TypeRoleGoldRepository.FindAsync(id);

            if (entity is null)
            {
                var errorMessage = string.Format(
                    Messages.NotFoundError, DataDictionary.TypeRoleGold);

                result.WithError(errorMessage);
            }
            else
            {
                context.HttpContext.Items[ProjectKeyName.ObjectKey] = entity;
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