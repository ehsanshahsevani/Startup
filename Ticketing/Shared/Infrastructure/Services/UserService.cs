using HttpServiceSeedworks;

namespace Infrastructure.Services;

public class UserService : HttpService
{
	public UserService(string baseUrl) : base(baseUrl)
	{
	}
}