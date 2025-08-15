namespace Utilities;

public static class GoldTools
{
	/// <summary>
	/// تبدیل مقدار عیار طلا به تومان با استفاده از قیمت فعلی طلا
	/// </summary>
	/// <param name="goldSoot">عیار طلا که باید تبدیل شود</param>
	/// <param name="goldPriceInThisTime">قیمت فعلی طلا به ازای هر گرم</param>
	/// <returns>مقدار طلا بر حسب تومان</returns>
	public static decimal GoldToToman(this decimal goldSoot, decimal goldPriceInThisTime)
	{
		var result =
			goldSoot * (goldPriceInThisTime / 1000);
		
		return result;
	}

	/// <summary>
	/// تبدیل تومان به مقدار طلای معادل بر اساس قیمت فعلی طلا
	/// </summary>
	/// <param name="toman">مقدار تومان که باید به طلا تبدیل شود</param>
	/// <param name="goldPriceInThisTime">قیمت فعلی طلا به ازای هر گرم</param>
	/// <returns>مقدار طلا معادل بر حسب گرم</returns>
	public static decimal TomanToGold(this decimal toman, decimal goldPriceInThisTime)
	{
		var result =
			Math.Round(toman / (goldPriceInThisTime / 1000), 2);
		
		return result;
	}

	/// <summary>
	/// تبدیل عدد وارد شده به درصد
	/// 0.5 به 0.05 درصد تبدیل میشود
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static decimal ConvertToPercentage(this decimal value)
	{
		var result =
			Math.Round(value / 100m, 3);
		
		return result;
	}

	/// <summary>
	/// محاسبه درصد مشخص از یک مقدار عددی
	/// مقدار درصد مشخص شده را به عنوان بخشی از عدد ورودی برمی‌گرداند
	/// </summary>
	/// <param name="value">مقدار عددی اصلی</param>
	/// <param name="percentage">درصد مورد نظر</param>
	/// <returns>نتیجه محاسبه عدد درصد مشخص شده از مقدار ورودی</returns>
	public static decimal CalculatePercentageOfAmount(this decimal value, decimal percentage)
	{
		var result =
			Math.Round(value * percentage, 2);
		
		return result;
	}

	/// <summary>
	/// محاسبه بخش کسری عدد وارد شده
	/// قسمت اعشاری عدد ورودی را بازمی‌گرداند
	/// </summary>
	/// <param name="value">مقدار عددی وارد شده</param>
	/// <returns>قسمت کسری عدد ورودی</returns>
	public static decimal GetFractionalDifference(this decimal value)
	{
		var result =
			Math.Round(value - Math.Floor(value), 2);
		
		return result;
	}

	/// <summary>
	/// تعداد طلا برای هر گرم
	/// </summary>
	/// <param name="value">تعداد طلا</param>
	/// <returns>تعداد طلا برای هر گرم</returns>
	public static decimal GoldPriceInThisTimeConfig(this decimal value)
	{
		var roundToTop =
			Math.Ceiling(value / 1000);
		
		var goldPriceInThisTime = roundToTop * 1000;
		
		return goldPriceInThisTime;
		
	}
}