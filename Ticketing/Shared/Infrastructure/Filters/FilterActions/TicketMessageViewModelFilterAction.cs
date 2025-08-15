using Constants;
using InfrastructureSeedworks.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence;
using Resources;
using SampleResult;
using ViewModels.Ticketing;
using Result = FluentResults.Result;

namespace Infrastructure.Filters.FilterActions;

public class TicketMessageViewModelFilterAction :
    IBaseAsyncActionFilter
{
    public TicketMessageViewModelFilterAction(IUnitOfWork unitOfWork)
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

        if (tryGetValue && modelObject is TicketMessageRequestViewModel model)
        {
            var resultModel = model.Validate();

            result.WithErrors(resultModel.Errors);

            if (result.IsSuccess)
                // 0.5. check for repeat data in database
                // var checkForAddResult =
                //     await UnitOfWork.TicketRepository.IsOkForAddAsync(model);
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
                        await UnitOfWork.TicketMessageRepository.FindAsync(model.Id);

                    if (entity is null)
                    {
                        var errorMessage =
                            string.Format(Messages.NotFoundError, DataDictionary.TicketMessage);

                        result.WithError(errorMessage);
                    }
                    else
                    {
                        var ticket =
                            await UnitOfWork.TicketRepository.FindAsync(entity.TicketId);
                        
                        ticket!.IsSeen = false;
                        
                        context.HttpContext.Items[ProjectKeyName.ObjectKey] = entity;
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