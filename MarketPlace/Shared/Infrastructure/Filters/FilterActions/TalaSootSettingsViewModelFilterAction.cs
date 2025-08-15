using Constants;
using Resources;
using Persistence;
using SampleResult;
using ViewModels.Marketplace;
using Microsoft.AspNetCore.Mvc;
using Result = FluentResults.Result;
using Microsoft.AspNetCore.Mvc.Filters;
using InfrastructureSeedworks.ActionFilter;

namespace Infrastructure.Filters.FilterActions;

public class TalaSootSettingsViewModelFilterAction : IBaseAsyncActionFilter
{
    public TalaSootSettingsViewModelFilterAction(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new Result();

        // **************************************************
        var tryGetValue =
            context.ActionArguments.TryGetValue("model", out var modelObject);
        // **************************************************

        if (tryGetValue == true && modelObject is TalaSootSettingsRequestViewModel model)
        {
            var resultModel = model.Validate();

            result.WithErrors(resultModel.Errors);

            if (result.IsSuccess == true)
            {
                // 0.5. check for repeat data in database
                // var checkForAddResult =
                //     await UnitOfWork.RoleGoldRepository.IsOkForAddAsync(model);
                //
                // result.WithErrors(checkForAddResult.Errors);

                // 1. check id
                // 2. get entity from database if exist
                // 3. save in context
                // 4. continue to action
                //////////////////////////////////////////////
                if (string.IsNullOrEmpty(model.Id) == false)
                {
                    var entity =
                        await UnitOfWork.TalaSootSettingsRepository.FindAsync(model.Id);

                    if (entity is null)
                    {
                        var errorMessage =
                            string.Format(Messages.NotFoundError, DataDictionary.TalaSootSettings);

                        result.WithError(errorMessage);
                    }
                    else
                    {
                        context.HttpContext.Items[ProjectKeyName.ObjectKey] = entity;
                    }
                }
            }
        }
        else
        {
            result.WithError(Messages.RequestNotValid);
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