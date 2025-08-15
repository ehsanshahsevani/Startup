using Microsoft.AspNetCore.Mvc;
using SampleResult;

namespace HttpServices.ProjectManager;

public class AccountingService : HttpServiceSeedworks.HttpService
{
	public AccountingService(string baseUrl) : base(baseUrl)
	{
		base.SetBaseApi(nameof(Resources.DataDictionary.Accounting));
	}

	#region POST: captcha-image/{subSystemPageId}

	/// <summary>
	///دریافت تصویر مربوط به کپتچا جهت ورود به سیستم
	/// ---
	/// User SubSystem: 55D304AD-AE71-4040-A323-0EBCBEDECF68
	/// Login SubSystemPage: 37A77F72-6D59-417F-8314-3C5B7B5D5B1A
	/// </summary>
	/// <param name="subSystemPageId">شناسه صفحه ای که برای ورود به آن به کپتچا نیاز دارید</param>
	/// <returns>تصویر</returns>
	public async Task<FileContentResult?> CaptchaImage(string subSystemPageId)
	{
		string url = $"captcha-image/{subSystemPageId}";

		var result =
			await PostAsync<object?, FileContentResult>(url, data: null);

		return result;
	}

	#endregion

	#region POST: login

	/// <summary>
	/// ورود و رفتن به مرحله تایید شماره موبایل
	/// </summary>
	/// <param name="phoneNumber">شماره تلفن</param>
	/// <param name="captchaCode">کد امنیتی</param>
	/// <returns></returns>
	public async Task<Result?> LoginAsync(string phoneNumber, string captchaCode)
	{
		string url = $"login";

		var result =
			await PostAsync<object, Result>(url, data: new
			{
				phoneNumber, captchaCode
			});

		return result;
	}

	#endregion

	#region POST: verify-otp

	/// <summary>
	/// تایید نهایی کد شماره موبایل و ایجاد توکن
	/// </summary>
	/// <param name="phoneNumber">شماره تلفن</param>
	/// <param name="otpCode">کد امنیتی</param>
	/// <returns>توکن ( جوت )</returns>
	public async Task<Result<string>?> VerifyOtpConfirm(string phoneNumber, string otpCode)
	{
		string url = $"verify-otp";

		var result =
			await PostAsync<object, Result<string>>(url, data: new
			{
				phoneNumber, otpCode
			});

		return result;
	}

	#endregion

	#region POST: resend-otp

	/// <summary>
	/// ارسال مجدد کد جهت تایید شماره تلفن
	/// </summary>
	/// <param name="phoneNumber"></param>
	/// <returns></returns>
	public async Task<Result?> ReSendOtpAsync(string phoneNumber)
	{
		string url = $"resend-otp";

		var result =
			await PostAsync<object, Result>(url, data: new
			{
				phoneNumber
			});

		return result;
	}

	#endregion
}