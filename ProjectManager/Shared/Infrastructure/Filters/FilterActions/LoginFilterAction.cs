using System.Text.RegularExpressions;
using Domain;
using Persistence;
using SampleResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using ViewModels;

namespace Infrastructure.Filters.FilterActions;

public class LoginFilterAction :
    InfrastructureSeedworks.ActionFilter.IBaseAsyncActionFilter
{
    public UserManager<User> UserManager { get; }

    public LoginFilterAction(UserManager<User> userManager)
    {
        UserManager = userManager;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = new FluentResults.Result();

        object? action =
            context.RouteData.Values[Constants.ProjectKeyName.ActionKey];
        
        object? controller
            = context.RouteData.Values[Constants.ProjectKeyName.ControllerKey];

        // **************************************************
        bool hasNumberPhone =
            context.ActionArguments.TryGetValue("phoneNumber", out object? numberPhoneValue);

        bool hasCaptchaCode =
            context.ActionArguments.TryGetValue("captchaCode", out object? captchaCodeValue);
        // **************************************************

        if (hasNumberPhone == false || hasCaptchaCode == false)
        {
            if (hasNumberPhone == false)
            {
                var errorMessage = string.Format(
                    Resources.Messages.RequiredError,
                    Resources.DataDictionary.PhoneNumber);
                
                result.WithError(errorMessage);
            }
            
            if (hasCaptchaCode == false)
            {
                var errorMessage = string.Format(
                    Resources.Messages.RequiredError,
                    Resources.DataDictionary.Captcha);
                
                result.WithError(errorMessage);
            }
        }
        else
        {
            string? numberPhone = numberPhoneValue as string;
            string? captchaCode = captchaCodeValue as string;

            if (string.IsNullOrWhiteSpace(numberPhone))
            {
                var errorMessage = string.Format(
                    Resources.Messages.RequiredError,
                    Resources.DataDictionary.PhoneNumber);
                
                result.WithError(errorMessage);
            }

            if (string.IsNullOrWhiteSpace(captchaCode))
            {
                var errorMessage = string.Format(
                    Resources.Messages.RequiredError,
                    Resources.DataDictionary.Captcha);
                
                result.WithError(errorMessage);
            }

            var checkRegex = Regex.IsMatch(
                numberPhone ?? string.Empty,
                Constants.RegularExpression.CellPhoneNumber);
            
            if (string.IsNullOrEmpty(numberPhone) == false && checkRegex == false)
            {
                result.WithError(Resources.Messages.PhoneNumberStartWithError);
            }
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