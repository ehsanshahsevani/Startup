using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class RoleResponseViewModel: BaseResponseViewModel<RoleRequestViewModel>
{
	public override RoleRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class RoleRequestViewModel : BaseRequestViewModel
{
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}