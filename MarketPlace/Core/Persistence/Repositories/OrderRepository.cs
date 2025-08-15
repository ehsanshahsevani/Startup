using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
	internal OrderRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}