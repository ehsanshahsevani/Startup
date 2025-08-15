using Microsoft.JSInterop;

namespace Infrastructure.IntropClasses;

public class BootstrapService : object
{
	public BootstrapService(IJSRuntime jSRuntime) : base()
	{
		JSRuntime = jSRuntime;
	}

	public IJSRuntime JSRuntime { get; }

	public async Task ShowModalError(string id)
	{
		await JSRuntime.InvokeVoidAsync(nameof(ShowModalError), id);
	}

	public async Task HideModalError()
	{
		await JSRuntime.InvokeVoidAsync(nameof(HideModalError));
	}

	public async Task ShowModalReference(string id)
	{
		await JSRuntime.InvokeVoidAsync(nameof(ShowModalReference), id);
	}

	public async Task HideModalReference()
	{
		await JSRuntime.InvokeVoidAsync(nameof(HideModalReference));
	}

	public async Task ShowModalInformation(string id)
	{
		await JSRuntime.InvokeVoidAsync(nameof(ShowModalInformation), id);
	}

	public async Task HideModalInformation()
	{
		await JSRuntime.InvokeVoidAsync(nameof(HideModalInformation));
	}

	public async Task ShowModalTimer(string id)
	{
		await JSRuntime.InvokeVoidAsync(nameof(ShowModalTimer), id);
	}

	public async Task HideModalTimer()
	{
		await JSRuntime.InvokeVoidAsync(nameof(HideModalTimer));
	}

	public async Task ShowModalQuestion(string id)
	{
		await JSRuntime.InvokeVoidAsync(nameof(ShowModalQuestion), id);
	}
	
	public async Task ToastBootstrapShow(string id)
	{
		await JSRuntime.InvokeVoidAsync(nameof(ToastBootstrapShow), id);
	}

	public async Task HideModalQuestion()
	{
		await JSRuntime.InvokeVoidAsync(nameof(HideModalQuestion));
	}
}
