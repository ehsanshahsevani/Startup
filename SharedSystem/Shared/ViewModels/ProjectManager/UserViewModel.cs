using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class UserResponseViewModel : BaseResponseViewModel<UserRequestViewModel>
{
	public override UserRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class UserRequestViewModel : BaseRequestViewModel
{
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}