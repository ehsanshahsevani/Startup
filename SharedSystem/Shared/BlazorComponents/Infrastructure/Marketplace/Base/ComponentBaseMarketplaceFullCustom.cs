using Microsoft.AspNetCore.Components;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace Infrastructure.Marketplace.Base;

/// <summary>
/// Represents a customizable base component for marketplace functionalities with a full custom implementation,
/// supporting request-response view model handling. This class provides mechanisms for managing and updating
/// the model state in scenarios involving lists of response objects and their corresponding request models.
/// </summary>
/// <typeparam name="TResponseList">The type for the response list, which must inherit from BaseResponseViewModel.</typeparam>
/// <typeparam name="TRequestModel">The type for the request model, which must inherit from BaseRequestViewModel and provide a default constructor.</typeparam>
public class ComponentBaseMarketplaceFullCustom<TResponseList, TRequestModel>
	: ComponentBaseMarketplaceWithListCustom<TResponseList, TRequestModel>
	where TResponseList : BaseResponseViewModel<TRequestModel>
	where TRequestModel : BaseRequestViewModel, new()
{
	public ComponentBaseMarketplaceFullCustom()
	{
		Model = new TRequestModel()
		{
			Ordering = 100_000
		};
	}

	[Inject] public NavigationManager NavigationManager { get; set; } = default!;
	
	[Parameter]
	public string? Id { get; set; }

	protected bool IsEditMode => string.IsNullOrEmpty(Id) == false;
	
	protected TRequestModel Model { get; set; }

	#region Page Structure

	protected void OnUpdateMode(TResponseList model)
	{
		Model = model.ToRequest();
	}

	protected void OffUpdateMode()
	{
		Model = new();

		var basePath =
			NavigationManager
				.ToBaseRelativePath(NavigationManager.Uri).Split('/').First();
		
		NavigationManager.NavigateTo($"/{basePath}", forceLoad: false);
	}
	
	protected async Task GoToPageAsync(string pageName, string? id)
	{
        ArgumentNullException.ThrowIfNull(pageName);

        Id = id ?? null;

        NavigationManager.NavigateTo($"/{pageName}/{Id}", forceLoad: false);
		
		await Task.CompletedTask;
	}
	
	#endregion /Page Structure
}