using Microsoft.JSInterop;

namespace Infrastructure.IntropClasses;

public class CookieService : object
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public CookieService(IJSRuntime jSRuntime) : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		JSRuntime = jSRuntime;
	}

	public event EventHandler TokenChangd;

	public IJSRuntime JSRuntime { get; }

	public async Task SetCookie(string name, string? value)
	{
		await JSRuntime.InvokeVoidAsync(identifier: nameof(SetCookie), name, value);

		TokenChangd?.Invoke(sender: TokenState.Add, e: EventArgs.Empty);
	}

	public async Task DeleteCookie(string name)
	{
		await JSRuntime.InvokeVoidAsync(identifier: nameof(DeleteCookie), name);

		TokenChangd?.Invoke(sender: TokenState.Remove, e: EventArgs.Empty);
	}

	public async Task<string?> GetCookie(string name)
	{
		string? token =
				await JSRuntime.InvokeAsync<string>(identifier: nameof(GetCookie), name);

		if (string.IsNullOrEmpty(token))
		{
			token = null;
		}

		return token;
	}
}

public enum TokenState
{
	None = 0,
	Add = 1,
	Remove = 2,
}
