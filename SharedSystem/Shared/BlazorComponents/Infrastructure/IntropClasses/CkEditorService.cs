using Microsoft.JSInterop;

namespace Infrastructure.IntropClasses;

public class CKEditorService : object
{
	public IJSRuntime JsRuntime { get; set; }

	public CKEditorService(IJSRuntime jsRuntime) : base()
	{
		JsRuntime = jsRuntime;
	}

	public async Task SetEditor(string id)
	{
		await JsRuntime.InvokeVoidAsync(nameof(SetEditor), id);
	}

	public async Task<string?> GetValue(string? id)
	{
		string? value = await JsRuntime
			.InvokeAsync<string?>(nameof(GetValue), id);

		return value;
	}

	public async Task SetValue(string id, string? value)
	{
		await JsRuntime.InvokeVoidAsync(nameof(SetValue), id, value);
	}
}
