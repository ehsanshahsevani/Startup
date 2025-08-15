using BlazorComponents.Shared.Modal;
using Microsoft.AspNetCore.Components;
using BaseProject.Model.ViewModel.Public;
using BlazorComponents.Services;

namespace Infrastructure.Marketplace.Base;

/// <summary>
/// A generic base class designed to extend functionality for marketplace components
/// with an associated model of type <typeparamref name="TRequestModel"/>.
/// Inherits from <see cref="ComponentBaseMarketplaceCustom"/>.
/// </summary>
/// <typeparam name="TRequestModel">
/// The type of the request model used by this component. Must have a parameterless constructor.
/// </typeparam>
public class ComponentBaseMarketplaceWithModelCustom<TRequestModel> : ComponentBaseMarketplaceCustom
	where TRequestModel : new()
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public ComponentBaseMarketplaceWithModelCustom() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
		Model = new TRequestModel();
	}

	[Inject] protected RequestResultService RequestResultService { get; set; } = default!;

	protected ModalLoading? LoadingModal;
	protected PageDescriptionModel PageDescription;
	protected ModalUpdateQuestion? ModalUpdateQuestion;

	protected TRequestModel? Model { get; set; }
}