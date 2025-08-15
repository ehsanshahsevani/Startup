using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using SampleResult;

namespace Infrastructure.Filters.FilterActions;

public class CheckCategoryIdActionFilter : InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public IUnitOfWork UnitOfWork { get; }

    public CheckCategoryIdActionFilter(IUnitOfWork unitOfWork)
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
            var category =
                await UnitOfWork
                    .CategoryRepository.FindAsync(id);

            if (category is null)
            {
                var errorMessage = string.Format(
                    Resources.Messages.NotFoundError, Resources.DataDictionary.Category);
                
                result.WithError(errorMessage);
            }
            else
            {
                context.HttpContext.Items[Constants.ProjectKeyName.ObjectKey] = category;
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