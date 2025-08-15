using SampleResult;

namespace BlazorComponents.Services;

public class RequestResultService : object
{
	public RequestResultService()
	{
		Results = new List<ResultPack>();
	}

	public event EventHandler<ResultPack>? OnAdded;

	public List<ResultPack> Results { get; private set; }

	public void AddResult(Result result)
	{
		var pack =
			new ResultPack(Guid.NewGuid().ToString(), result);
		
		Results.Add(pack);
		
		OnAdded?.Invoke(this, pack);
	}

	public void RemoveResult(string id)
	{
		var item =
			Results.FirstOrDefault(x => x.Id == id);
		
		if (item is not null)
		{
			Results.Remove(item);
		}
	}
}

public record ResultPack(string Id, Result Result);