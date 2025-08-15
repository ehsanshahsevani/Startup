using SampleResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters.FilterActions;

public class CheckEntityUserIdFilterAction :
    InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    // public UserManager<User> UserManager { get; }
    //
    // public CheckEntityUserIdFilterAction(UserManager<User> userManager)
    // {
    //     UserManager = userManager;
    // }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new FluentResults.Result();
        //
        // object? action = context.RouteData.Values["action"];
        // object? controller = context.RouteData.Values["controller"];
        //
        // string? model =
        //     context.ActionArguments.FirstOrDefault
        //     (current =>
        //         current.Value is string).Value as string;
        //
        // if (string.IsNullOrEmpty(model) == true)
        // {
        //     result.WithError(Resources.Messages.RequestNotValid);
        // }
        // else
        // {
        //     var resultSearch =
        //         await UserManager.Users
        //             .Include(current => current.MarketPlaceProfile)
        //             .Where(current => current.IsDeleted == false)
        //             .Where(current => current.IsActive == true)
        //             .Where(current => current.Id == model)
        //             .FirstOrDefaultAsync();
        //
        //     if (resultSearch is null)
        //     {
        //         string errorMessage = string.Format(
        //             Resources.Messages.NotFoundError, Resources.DataDictionary.User);
        //
        //         result.WithError(errorMessage);
        //     }
        //     else
        //     {
        //         context.HttpContext.Items[Constants.ProjectKeyName.ObjectKey] = resultSearch;
        //     }
        // }

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