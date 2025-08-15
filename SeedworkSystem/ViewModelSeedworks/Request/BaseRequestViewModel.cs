using SampleResult;
using ViewmodelSeedworks.Base;

namespace ViewmodelSeedworks.Request;

public abstract class BaseRequestViewModel : BaseViewModel, IValidator
{
    public abstract Result Validate();
}
