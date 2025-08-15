using RequestFeatures;
using Utilities;

namespace BaseProject.Model.ViewModel.Public;

/// <summary>
/// DELETE Modal Confirm , Show Modal BootStrap For Delete Question
/// </summary>
/// <param name="Controller"></param>
/// <param name="Action"></param>
/// <param name="Id"></param>
/// <param name="ModalId"></param>
public record DeleteConfirmModel
	(string Controller, string Action, int Id, string ModalId, string? Text = null);


/// <summary>
/// Update Modal Confirm, Show Modal Bootstrap For Update Question
/// </summary>
/// <param name="FormId"></param>
/// <param name="ModalId"></param>
public record UpdateConfirmModel
	(string FormId, string ModalId);

/// <summary>
/// UI Select Input, Auto Complete, Select, Input, Radio Button, ...
/// </summary>
public class UiSelectModel
{
	public UiSelectModel(string value, string valueId, bool selected = false)
	{
		Value = value;
		ValueId = valueId;
		Selected = selected;
	}

	public UiSelectModel(string value, string valueId)
	{
		Value = value;
		ValueId = valueId;
		Selected = false;
	}
	
	public UiSelectModel(){}

	public string Value { get; init; }
	public string ValueId { get; init; }
	public bool Selected { get; init; }
}

/// <summary>
/// UI Table Information - Pagination System
/// </summary>
/// <param name="Area"></param>
/// <param name="Controller"></param>
/// <param name="Action"></param>
/// <param name="TableInformation"></param>
public record TableInformationModel
	(string Area, string Controller, string Action, MetaData? TableInformation);

/// <summary>
/// UI Table Information - Pagination System
/// </summary>
/// <param name="Route"></param>
/// <param name="TableInformation"></param>
public record TableInformationModelRoute
	(string Route, MetaData? TableInformation);

public record PageDescriptionModel
	(string PageTitle, string? Header, List<string> Descriptions, Dictionary<string, string>? FooterLink);


/// <summary>
/// Use In Search Box Top Table -> _SearchBox.cshtml [PartialViwe]
/// </summary>
/// <param name="Area"></param>
/// <param name="Controller"></param>
/// <param name="Action"></param>
/// <param name="text"></param>
public record SearchBoxModel(
	string Area,
	string Controller,
	string Action = "Search",
	string? ParameterName = "text");
