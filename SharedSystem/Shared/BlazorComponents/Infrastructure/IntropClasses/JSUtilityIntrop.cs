using Microsoft.JSInterop;

namespace Infrastructure.IntropClasses;

public class JSUtilityIntrop : object
{
	public JSUtilityIntrop(IJSRuntime jSRuntime) : base()
	{
		JSRuntime = jSRuntime;
	}

	public IJSRuntime JSRuntime { get; }

	public async Task BackOnHistory()
	{
		await JSRuntime.InvokeVoidAsync(nameof(BackOnHistory));
	}

	public async Task AutoComplete(string inputId, string[] arr)
	{
		await JSRuntime.InvokeVoidAsync(nameof(AutoComplete), inputId, arr);
	}

	public async Task CopyTextToClipboard(string? text)
	{
		await JSRuntime.InvokeVoidAsync(nameof(CopyTextToClipboard), text);
	}
}
