namespace Enums.Marketplace;

/// <summary>
/// نوع دارایی میتواند
/// دارایی پول باشد
/// یا دارایی طلا
/// </summary>
public enum AssetsType : byte
{
	/// <summary>
	/// پول
	/// </summary>
	Money = 0,
	
	/// <summary>
	/// طلا
	/// </summary>
	Gold = 1,
}