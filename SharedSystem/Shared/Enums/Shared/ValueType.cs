namespace Enums.Shared;

/// <summary>
/// نوع مقداردهی: قیمت یا درصد
/// </summary>
public enum ValueType : byte
{
	/// <summary>
	/// مقدار بر حسب قیمت (ریال یا تومان)
	/// </summary>
	Price = 0,

	/// <summary>
	/// مقدار بر حسب درصد
	/// </summary>
	Percent = 1
}