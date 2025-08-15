using Persistence.Abstracts;
using Persistence.Repositories;

namespace Persistence;

public class UnitOfWork : Base.UnitOfWork, IUnitOfWork
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public UnitOfWork(Tools.Options options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
    
    private ISubSystemLocalRepository _subSystemLocalRepository;
    public ISubSystemLocalRepository SubSystemLocalRepository
    {
        get
        {
            _subSystemLocalRepository ??= new SubSystemLocalRepository(DatabaseContext);
            return _subSystemLocalRepository;
        }
    }
}