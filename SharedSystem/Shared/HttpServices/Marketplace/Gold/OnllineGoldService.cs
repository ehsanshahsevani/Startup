using InfrastructureSeedworks;
using Utilities;

namespace HttpServices.Marketplace.Gold;

public class OnlineGoldService : HttpServiceSeedworks.HttpService
{
	public OnlineGoldService() : base(ServerSettings.DomainGoldApi)
	{
	}
	
	public async Task<decimal> GoldPriceInThisTime()
	{
		var result =
			7_152_203m.GoldPriceInThisTimeConfig();

		return result;
	}
}