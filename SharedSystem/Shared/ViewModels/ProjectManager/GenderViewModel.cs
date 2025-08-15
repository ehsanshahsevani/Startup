using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class GenderResponseViewModel: BaseResponseViewModel<GenderRequestViewModel>
{
	public override GenderRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class GenderRequestViewModel : BaseRequestViewModel
{
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}