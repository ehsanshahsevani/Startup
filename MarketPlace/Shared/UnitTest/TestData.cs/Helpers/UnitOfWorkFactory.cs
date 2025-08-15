using Persistence.Tools;
using Persistence.Tools.Enums;

namespace Persistence.Tests.Helpers;

public static class UnitOfWorkFactory
{
	public static IUnitOfWork Create()
	{
		var options = new Options("fakeDatabase")
		{
			Provider = Provider.InMemory,
			
			// هر بار که تست ما اجرا میشود یک دیتابیس جدا برایش ساخته میشود
			DatabaseName = Guid.NewGuid().ToString()
		};

		return new UnitOfWork(options);
	}
}