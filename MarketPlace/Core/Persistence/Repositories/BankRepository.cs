using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class BankRepository : Repository<Bank>, IBankRepository
{
	internal BankRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}