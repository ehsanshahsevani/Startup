using Microsoft.JSInterop;

namespace Infrastructure.IntropClasses;

public class GooglereCaptchaService
{
	public GooglereCaptchaService(IJSRuntime jSRuntime) : base()
	{
		JSRuntime = jSRuntime;
	}

	public IJSRuntime JSRuntime { get; }
}
