using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
	internal OrderItemRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}