using System.Text.Json;
using DomainSeedworks.Log;
using DatabaseContextSeedworks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PersistenceSeedworks.LogManager;

/// <summary>
/// base log deatil POCO file !
/// </summary>
public class LogDetailManager<TDbContext> : ILogDetailManager where TDbContext : DbContext
{
	public TDbContext DbContext { get; }

	public LogDetailManager(TDbContext dbContext) : base()
	{
		DbContext = dbContext;
	}

   public async Task CreateAsync(LogDetail logDetail)
    {
    	// int result = 0;

    	var detailsLogs = new List<LogDetail>();

    	var entries = DbContext.ChangeTracker.Entries();

    	var jsonSerializerOptions =
    		new JsonSerializerOptions()
    		{
    			ReferenceHandler = ReferenceHandler.IgnoreCycles,
    		};

    	foreach (var item in entries)
    	{
    		if (item.State == EntityState.Unchanged)
    		{
    			continue;
    		}

    		logDetail.EntityTracker = item.Entity;

    		// detailsLog.DebugView = item.DebugView.LongView;

    		logDetail.StateChangeWorker = item.State.ToString();

    		logDetail.NameSpace = item.Entity.ToString();
    		logDetail.TypeName = item.Entity.GetType().Name;

    		if (string.IsNullOrEmpty(logDetail.TypeName) == false
    			&& logDetail.TypeName != $"{nameof(LogServer)}"
    			&& logDetail.TypeName != $"{nameof(logDetail)}")
    		{
    			detailsLogs.Add(logDetail.Clone<LogDetail>());
    		}
    	}

    	// result =
    		await DbContext.SaveChangesAsync();

    	if (detailsLogs.Count != 0)
    	{
    		detailsLogs.ForEach(item =>
    		{
    			item.JsonField =
    				JsonSerializer.Serialize(item.EntityTracker, jsonSerializerOptions);

    			item.RecordId = (item.EntityTracker as DomainSeedworks.BaseEntity)!.Id;
    		});

    		await DbContext.AddRangeAsync(detailsLogs);
    		await DbContext.SaveChangesAsync();
    	}

    	// return result;
    }
}