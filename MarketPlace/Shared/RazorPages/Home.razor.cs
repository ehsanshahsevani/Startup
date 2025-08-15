using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RazorPages;

public partial class Home : ComponentBase
{
	[Inject] private IJSRuntime JsRuntime { get; set; } = default!;
	
	private readonly List<CategoryStats> _categories =
	[
		new() { Name = "مبلمان", ActiveCount = 450, InactiveCount = 30, DeletedCount = 20 },
		new() { Name = "دکور و تزئینات", ActiveCount = 320, InactiveCount = 25, DeletedCount = 15 },
		new() { Name = "لوازم روشنایی", ActiveCount = 280, InactiveCount = 20, DeletedCount = 10 },
		new() { Name = "فرش و موکت", ActiveCount = 200, InactiveCount = 15, DeletedCount = 5 },
		new() { Name = "پرده و لوازم پرده", ActiveCount = 150, InactiveCount = 10, DeletedCount = 8 }
	];

	private IEnumerable<CategoryStats> SortedCategories => _categories.OrderByDescending(c => c.TotalCount);

	private class CategoryStats
	{
		public string Name { get; set; } = "";
		public int ActiveCount { get; set; }
		public int InactiveCount { get; set; }
		public int DeletedCount { get; set; }
		public int TotalCount => ActiveCount + InactiveCount + DeletedCount;
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await JsRuntime.InvokeVoidAsync("showNemodar");
		}
	}
}