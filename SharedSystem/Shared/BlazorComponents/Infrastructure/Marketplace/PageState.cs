namespace Infrastructure.Marketplace;

public class PageState : object
{
	public string? ShopId { get; set; }
	public bool ShopLocked => string.IsNullOrEmpty(ShopId) == false;
	
	public string? ProductTitleId { get; set; }
	public bool ProductTitleLocked => string.IsNullOrEmpty(ProductTitleId) == false;
}