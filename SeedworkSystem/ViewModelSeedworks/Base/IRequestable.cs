using ViewmodelSeedworks.Request;

namespace ViewmodelSeedworks.Base;

public interface IRequestable<T> where T: BaseRequestViewModel
{
	T ToRequest();
}