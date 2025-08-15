using Domain;
using SkiaSharp;
using AutoMapper;
using Persistence;
using FluentResults;
using DecoyabServices;
using JwtSettings.Models;
using Microsoft.AspNetCore.Mvc;
using ViewModels.ProjectManager;
using RestFullApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks.LogManager;
using Infrastructure.Filters.FilterActions;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت حساب کاربری
/// </summary>
public class AccountingController : BaseControllerIdentity
{
    #region DI Settings & Constructor

    public IMessageService MessageService { get; }

    public AccountingController(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        HttpClient httpClient,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        SignInManager<User> signInManager,
        ILogDetailManager logDetailManager,
        ILogServerManager logServerManager,
        IMessageService messageService) : base(mapper, unitOfWork, httpClient, configuration,
        httpContextAccessor,
        userManager, roleManager, signInManager, logDetailManager, logServerManager)
    {
        MessageService = messageService;
    }

    #endregion

    #region POST : /login

    /// <summary>
    /// ورود به سیستم
    /// </summary>
    /// <param name="phoneNumber">شماره تلفن</param>
    /// <param name="captchaCode">کد امنیتی</param>
    /// <returns></returns>
    [HttpPost(template: "login")]
    [ServiceFilter<LoginFilterAction>]
    public async Task<IActionResult> LoginAsync(string phoneNumber, string captchaCode)
    {
        var result = new Result();

        var ipAddress =
            HttpContext.Connection.RemoteIpAddress?.ToString();

        if (ipAddress is null)
        {
            throw new NullReferenceException(nameof(ipAddress));
        }

        // var chapcha =
        //     await UnitOfWork
        //         .CaptchaRepository.GetCaptchaByTextAndIpAsync(captchaCode, ipAddress);
        //
        // if (chapcha is null)
        // {
        //     result.WithError(Resources.Messages.CaptchaNotFound);
        // }
        // else
        // {
        //     chapcha.IsActive = false;
        //     chapcha.IsDeleted = true;
        // }

        if (result.IsSuccess == true)
        {
            var optCode = new Random().Next(101010, 999999);

            var user = await UserManager.FindByNameAsync(phoneNumber);

            if (user is null)
            {
                user = new User
                {
                    Email = null,
                    IsActive = true,
                    IsDeleted = false,
                    UpdateDateTime = DateTime.Now,

                    OtpCode = optCode.ToString(),

                    EmailConfirmed = true,
                    UserName = phoneNumber,
                    PhoneNumber = phoneNumber,
                    PhoneNumberConfirmed = false,
                };
                
                await UserManager.CreateAsync
                    (user, Guid.NewGuid().ToString());
            }
            else
            {
                user.OtpCode = optCode.ToString();
                user.UpdateDateTime = DateTime.Now;

                await UserManager.UpdateAsync(user);
            }

            await MessageService
                .SendLookupAsync(SmsDetails.ApiKey,
                    SmsDetails.LoginPattern, phoneNumber,
                    user.OtpCode);

            result.WithSuccess(Resources.Messages.SendCodeSuccess);
        }

        return FluentResult(result);
    }

    #endregion

    #region POST : /resend-otp

    /// <summary>
    /// ارسال مجدد کد
    /// </summary>
    /// <param name="phoneNumber">شماره تلفن</param>
    /// <returns>کد یکبار مصرف</returns>
    [HttpPost(template: "resend-otp")]
    [ServiceFilter<CheckPhoneNumberFilterAction>]
    public async Task<IActionResult> ReSendOtpAsync(string phoneNumber)
    {
        var result = new Result();

        var user =
            HttpContext.Items[Constants.ProjectKeyName.ObjectKey] as User;

        if (user!.UpdateDateTime.AddMinutes(2) > DateTime.Now)
        {
            var optCode =
                new Random().Next(101010, 999999);

            user.OtpCode = optCode.ToString();
            user.UpdateDateTime = DateTime.Now;

            await UserManager.UpdateAsync(user);

            await MessageService
                .SendLookupAsync(apiKey: SmsDetails.ApiKey,
                    SmsDetails.LoginPattern, user.PhoneNumber!,
                    user.OtpCode);

            result.WithSuccess(Resources.Messages.SendCodeSuccess);
        }
        else
        {
            result.WithError(Resources.Messages.BeforeCodeIsActive);
        }

        return FluentResult(result);
    }

    #endregion
    
    #region POST : /verify-otp

    /// <summary>
    /// تایید کد نهایی و دریافت توکن
    /// </summary>
    /// <param name="phoneNumber">شماره تلفن</param>
    /// <param name="otpCode">کد یکبار مصرف</param>
    /// <returns>توکن با امضای سرور دکویاب</returns>
    [HttpPost(template: "verify-otp")]
    [ServiceFilter<CheckPhoneNumberFilterAction>]
    public async Task<IActionResult> VerifyOtpConfirm(string phoneNumber, string otpCode)
    {
        var result = new Result<string>();

        if (string.IsNullOrEmpty(otpCode) == true)
        {
            var message = string.Format(
                Resources.Messages.RequiredError, Resources.DataDictionary.OtpCode);

            result.WithError(message);
        }

        var user =
            HttpContext.Items[Constants.ProjectKeyName.ObjectKey] as User;

        var expireTime = user!.UpdateDateTime.AddMinutes(2);
        
        if (expireTime < DateTime.Now)
        {
            result.WithError(Resources.Messages.OtpCodeIsInvalid);
        }
        else
        {
            var jwt = new Jwt();
            
            Configuration.Bind(key: nameof(Jwt), jwt);
            
            string token =
                JwtSettings.JwtUtility.GenerateJwtToken(user, jwt);

            result.WithValue(token);
         
            user.OtpCode = string.Empty;
            await UserManager.UpdateAsync(user);
            
            result.WithSuccess(Resources.Messages.SuccessLoginMessage);
        }

        return FluentResult(result);
    }

    #endregion
}