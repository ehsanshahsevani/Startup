using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class UserRelationRepository : Repository<UserRelation> , IUserRelationRepository
{
    internal UserRelationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}