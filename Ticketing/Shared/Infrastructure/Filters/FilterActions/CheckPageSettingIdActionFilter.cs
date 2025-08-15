using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using SampleResult;

namespace Infrastructure.Filters.FilterActions;

public class CheckPageSettingIdActionFilter : InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public IUnitOfWork UnitOfWork { get; }

    public CheckPageSettingIdActionFilter(IUnitOfWork unitOfWork)
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
            var entity =
                await UnitOfWork
                    .PageSettingRepository.FindAsync(id);

            if (entity is null)
            {
                var errorMessage = string.Format(
                    Resources.Messages.NotFoundError, Resources.DataDictionary.PageSetting);
                
                result.WithError(errorMessage);
            }
            else
            {
                context.HttpContext.Items[Constants.ProjectKeyName.ObjectKey] = entity;
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