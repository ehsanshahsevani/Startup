using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using SampleResult;
using ViewModels.Marketplace;

namespace Infrastructure.Filters.FilterActions;

public class ProductViewModelFilterAction :
    InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public IUnitOfWork UnitOfWork { get; }

    public ProductViewModelFilterAction(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new FluentResults.Result();

        // **************************************************
        bool tryGetValue =
            context.ActionArguments.TryGetValue("model", out object? modelObject);
        // **************************************************

        if (tryGetValue == true && modelObject is ProductRequestViewModel model)
        {
            var resultModel = model.Validate();

            result.WithErrors(resultModel.Errors);

            if (result.IsSuccess == true)
            {
                // 0.5. check for repeat data in database
                var checkForAddResult =
                    await UnitOfWork.ProductRepository.IsOkForAddAsync(model);

                result.WithErrors(checkForAddResult.Errors);

                // 1. check id
                // 2. get entity from database if exist
                // 3. save in context
                // 4. continue to action
                //////////////////////////////////////////////
                if (string.IsNullOrEmpty(model.Id) == false)
                {
                    var entity =
                        await UnitOfWork.ProductRepository.FindAsync(model.Id);

                    if (entity is null)
                    {
                        var errorMessage =
                            string.Format(Resources.Messages.NotFoundError, Resources.DataDictionary.Product);

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