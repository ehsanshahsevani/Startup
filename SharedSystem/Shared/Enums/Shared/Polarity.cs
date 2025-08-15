/// <summary>
/// مشخص‌کننده حالت مثبت یا منفی
/// </summary>
public enum Polarity : int
{
	/// <summary>
	/// منفی
	/// </summary>
	Negative = -1,
	
	/// <summary>
	/// حالت خنثی یا نامشخص (مثلاً زمانی که داده‌ای در دسترس نیست)
	/// </summary>
	Neutral = 0,
	
	/// <summary>
	/// مثبت
	/// </summary>
	Positive = 1
}