using RequestFeatures;
using BlazorComponents.Shared;
using BlazorComponents.Services;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using BlazorComponents.Shared.Modal;
using Microsoft.AspNetCore.Components;
using BaseProject.Model.ViewModel.Public;

namespace Infrastructure.Marketplace.Base;

/// <summary>
/// Represents a base class for marketplace components that manage a customizable list of records.
/// This class provides functionality to handle operations such as adding, updating, and removing records
/// in a generic list, along with support for pagination and modal interactions.
/// </summary>
/// <typeparam name="TResponseList">The type of the response list, which extends <see cref="BaseResponseViewModel{TRequestModel}"/>.</typeparam>
/// <typeparam name="TRequestModel">The type of the request model, which extends <see cref="BaseRequestViewModel"/>.</typeparam>
public class ComponentBaseMarketplaceWithListCustom<TResponseList, TRequestModel>
	: ComponentBaseMarketplaceCustom
	where TResponseList : BaseResponseViewModel<TRequestModel>
	where TRequestModel : BaseRequestViewModel, new()

{
	public ComponentBaseMarketplaceWithListCustom()
	{
		List = null;
	}

	[Inject] protected NavigationManager NavigationManager { get; set; } = default!;
	[Inject] protected RequestResultService RequestResultService { get; set; } = default!;
	[Inject] protected BlazorComponents.Services.UrlQueryService UrlQueryService { get; set; } = default!;
	
	protected List<TResponseList>? List { get; set; }

	protected MetaData? _metaData;
	protected PageDescriptionModel _pageDescription;

	protected ModalLoading? _loadingModal;
	protected PaginationSystem _paginationSystem;
	protected ModalDeleteQuestion? _modalDeleteQuestion;
	protected ModalUpdateQuestion? _modalUpdateQuestion;
	
	#region Page Structure
	
	protected void UpdateRecordInTable(TResponseList model)
	{
		var index = List?.FindIndex(x => x.Id == model.Id);

		if (index is not null)
		{
			if (List is not null)
			{
				List[index.Value] = model;
			}
		}
	}

	protected void RemoveRecordInTable(string id)
	{
		List?.RemoveAll(x => x.Id == id);
	}

	protected void InsertRecordInTable(TResponseList model)
	{
		List?.Insert(index: 0, model);
	}

	#endregion /Page Structure
}