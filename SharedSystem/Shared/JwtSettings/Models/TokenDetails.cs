namespace JwtSettings.Models;


public class TokenDetails : object
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public TokenDetails() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		TokenIsOk = false;
		RoleNames = new List<string>();
		PageHrefs = new List<string>();
	}

	public string? Token { get; set; }

	public int UserId { get; set; }
	public string UserName { get; set; }

	public List<string> RoleNames { get; set; }
	public List<string> PageHrefs { get; set; }

	public bool TokenIsOk { get; set; }
}
