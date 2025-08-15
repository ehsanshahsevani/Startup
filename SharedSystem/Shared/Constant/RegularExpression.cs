namespace Constants;

/// <summary>
/// https://regex101.com/
/// </summary>
public static class RegularExpression
{
	static RegularExpression()
	{
	}

	public const string NationalCode = @"^\d{10}$";
	public const string CellPhoneNumber = @"^09\d{9}";
	public const string Username = @"^[a-zA-Z0-9_]{6,20}$";
	public const string Password = @"^[a-zA-Z0-9_]{8,20}$";
	public const string AToZDigitsUnderline = @"^[a-zA-Z][a-zA-Z0-9_]*$";
	public const string EmailAddress = @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9_.-]+$";

	public const string IP =
		@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

	public const string Url =
		@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

	public const string DateTime =
		@"^[1-4]\d{3}\/((0[1-6]\/((3[0-1])|([1-2][0-9])|(0[1-9])))|((1[0-2]|(0[7-9]))\/(30|31|([1-2][0-9])|(0[1-9]))))$";

	public const string Time = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";

}
