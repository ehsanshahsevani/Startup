using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
	internal TransactionRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}