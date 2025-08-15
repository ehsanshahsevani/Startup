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

private IActionRepository _actionRepository;

public IActionRepository ActionRepository
{
	get
	{
		_actionRepository ??= new ActionRepository(DatabaseContext);
		return _actionRepository;
	}
}
// **************************************************

private ICommutingRepository _commutingRepository;

public ICommutingRepository CommutingRepository
{
	get
	{
		_commutingRepository ??= new CommutingRepository(DatabaseContext);
		return _commutingRepository;
	}
}
// **************************************************

private IDashboardRepository _dashboardRepository;

public IDashboardRepository DashboardRepository
{
	get
	{
		_dashboardRepository ??= new DashboardRepository(DatabaseContext);
		return _dashboardRepository;
	}
}
// **************************************************

private IDashboardPageRoleRepository _dashboardPageRoleRepository;

public IDashboardPageRoleRepository DashboardPageRoleRepository
{
	get
	{
		_dashboardPageRoleRepository ??= new DashboardPageRoleRepository(DatabaseContext);
		return _dashboardPageRoleRepository;
	}
}
// **************************************************

private INotificationRepository _notificationRepository;

public INotificationRepository NotificationRepository
{
	get
	{
		_notificationRepository ??= new NotificationRepository(DatabaseContext);
		return _notificationRepository;
	}
}
// **************************************************

private IReferalLogRepository _referalLogRepository;

public IReferalLogRepository ReferalLogRepository
{
	get
	{
		_referalLogRepository ??= new ReferalLogRepository(DatabaseContext);
		return _referalLogRepository;
	}
}
// **************************************************

private IServerRepository _serverRepository;

public IServerRepository ServerRepository
{
	get
	{
		_serverRepository ??= new ServerRepository(DatabaseContext);
		return _serverRepository;
	}
}
// **************************************************

private ISubSystemRepository _subSystemRepository;

public ISubSystemRepository SubSystemRepository
{
	get
	{
		_subSystemRepository ??= new SubSystemRepository(DatabaseContext);
		return _subSystemRepository;
	}
}
// **************************************************

private ISubSystemRoleAccessRepository _subSystemRoleAccessRepository;

public ISubSystemRoleAccessRepository SubSystemRoleAccessRepository
{
	get
	{
		_subSystemRoleAccessRepository ??= new SubSystemRoleAccessRepository(DatabaseContext);
		return _subSystemRoleAccessRepository;
	}
}
// **************************************************

private IUserRelationRepository _userRelationRepository;

public IUserRelationRepository UserRelationRepository
{
	get
	{
		_userRelationRepository ??= new UserRelationRepository(DatabaseContext);
		return _userRelationRepository;
	}
}
// **************************************************

private IUserRelationTempRepository _userRelationTempRepository;

public IUserRelationTempRepository UserRelationTempRepository
{
	get
	{
		_userRelationTempRepository ??= new UserRelationTempRepository(DatabaseContext);
		return _userRelationTempRepository;
	}
}
// **************************************************
}