using Persistence.Abstracts;

namespace Persistence;

public interface IUnitOfWork : PersistenceSeedworks.IUnitOfWork
{
	public ISubSystemLocalRepository SubSystemLocalRepository { get; }
}