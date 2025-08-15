using Microsoft.EntityFrameworkCore;

namespace RequestFeatures;

public class PagedList<T> : List<T>
{
	public MetaData MetaData { get; set; }

	public PagedList(List<T> items, int count, int pageNumber, int pageSize, string? text = null) : base()
	{
		MetaData = new MetaData()
		{
			TotalCount = count,
			PageSize = pageSize,
			CurrentPage = pageNumber,
			TotalPages = (int)Math.Ceiling(count / (double)pageSize),
		};

		AddRange(items);
	}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private PagedList() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
	}

	public static PagedList<T> ToPagedList
		(IEnumerable<T> source, int pageNumber, int pageSize)
	{
		var count = source.Count();

		var items =
			source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		var result =
			new PagedList<T>(items, count, pageNumber, pageSize);

		return result;
	}

	public static async Task<PagedList<T>> ToPagedList(
		IQueryable<T> source, RequestParameters parameters,
		CancellationToken cancellationToken = default)
	{
		var count = await source.CountAsync(cancellationToken: cancellationToken);

		var items = await source

			.Skip((parameters.PageNumber - 1) * parameters.PageSize)
			
			.Take(parameters.PageSize)
			
			.ToListAsync(cancellationToken: cancellationToken);

		var result =
			new PagedList<T>(items, count, parameters.PageNumber, parameters.PageSize, parameters.Text);

		return result;
	}
}
