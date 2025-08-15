namespace Enums.Shared;

/// <summary>
/// مشخص‌کننده نوع تغییر: کاهشی یا افزایشی
/// </summary>
public enum ChangeDirection : int
{
	/// <summary>
	/// کاهشی
	/// </summary>
	Decrease = -1,
	
	/// <summary>
	/// بدون تغییر (مقدار قبلی و فعلی یکسان است)
	/// </summary>
	NoChange = 0,
	
	/// <summary>
	/// افزایشی
	/// </summary>
	Increase = 1
}