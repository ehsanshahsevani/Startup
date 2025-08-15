using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class ProvinceResponseViewModel: BaseResponseViewModel<ProvinceRequestViewModel>
{
	public override ProvinceRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class ProvinceRequestViewModel : BaseRequestViewModel
{
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}