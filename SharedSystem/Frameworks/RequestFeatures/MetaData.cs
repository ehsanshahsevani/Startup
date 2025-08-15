namespace RequestFeatures;

public class MetaData : object
{
	public MetaData() : base()
	{
	}

	public int CurrentPage { get; set; }

	public int TotalPages { get; set; }

	public int PageSize { get; set; }

	public int TotalCount { get; set; }
	
	public bool HasPrevious()
	{
		var result = CurrentPage > 1;
		return result;
	}

	public bool HasNext()
	{
		var result = CurrentPage < TotalPages;
		return result;
	}
}
