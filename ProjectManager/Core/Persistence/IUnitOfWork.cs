using Persistence.Abstracts;

namespace Persistence;

public interface IUnitOfWork : PersistenceSeedworks.IUnitOfWork
{
    IActionRepository ActionRepository { get; }
    ICommutingRepository CommutingRepository { get; }
    IDashboardRepository DashboardRepository { get; }
    IDashboardPageRoleRepository DashboardPageRoleRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IReferalLogRepository ReferalLogRepository { get; }
    IServerRepository ServerRepository { get; }
    ISubSystemRepository SubSystemRepository { get; }
    ISubSystemRoleAccessRepository SubSystemRoleAccessRepository { get; }
    IUserRelationRepository UserRelationRepository { get; }
    IUserRelationTempRepository UserRelationTempRepository { get; }
}