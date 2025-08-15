using Persistence.Abstracts;

namespace Persistence;

public interface IUnitOfWork : PersistenceSeedworks.IUnitOfWork
{
    IAttachmentRepository AttachmentRepository { get; }
    IAttachmentSubjectRepository AttachmentSubjectRepository { get; }
    IPageSettingRepository PageSettingRepository { get; }
    IStatusRepository StatusRepository { get; }
    ISubSystemLocalRepository SubSystemLocalRepository { get; }
    ITicketRepository TicketRepository { get; }
    ITicketMessageRepository TicketMessageRepository { get; }
    ITicketSubjectRepository TicketSubjectRepository { get; }
}