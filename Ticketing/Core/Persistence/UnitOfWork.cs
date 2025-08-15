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

	private IAttachmentRepository _attachmentRepository;

	public IAttachmentRepository AttachmentRepository
	{
		get
		{
			_attachmentRepository ??= new AttachmentRepository(DatabaseContext);
			return _attachmentRepository;
		}
	}
// **************************************************

	private IAttachmentSubjectRepository _attachmentSubjectRepository;

	public IAttachmentSubjectRepository AttachmentSubjectRepository
	{
		get
		{
			_attachmentSubjectRepository ??= new AttachmentSubjectRepository(DatabaseContext);
			return _attachmentSubjectRepository;
		}
	}
// **************************************************

	private IPageSettingRepository _pageSettingRepository;

	public IPageSettingRepository PageSettingRepository
	{
		get
		{
			_pageSettingRepository ??= new PageSettingRepository(DatabaseContext);
			return _pageSettingRepository;
		}
	}
// **************************************************

	private IStatusRepository _statusRepository;

	public IStatusRepository StatusRepository
	{
		get
		{
			_statusRepository ??= new StatusRepository(DatabaseContext);
			return _statusRepository;
		}
	}
// **************************************************

	private ISubSystemLocalRepository _subSystemLocalRepository;

	public ISubSystemLocalRepository SubSystemLocalRepository
	{
		get
		{
			_subSystemLocalRepository ??= new SubSystemLocalRepository(DatabaseContext);
			return _subSystemLocalRepository;
		}
	}
// **************************************************

	private ITicketRepository _ticketRepository;

	public ITicketRepository TicketRepository
	{
		get
		{
			_ticketRepository ??= new TicketRepository(DatabaseContext);
			return _ticketRepository;
		}
	}
// **************************************************

	private ITicketMessageRepository _ticketMessageRepository;

	public ITicketMessageRepository TicketMessageRepository
	{
		get
		{
			_ticketMessageRepository ??= new TicketMessageRepository(DatabaseContext);
			return _ticketMessageRepository;
		}
	}
// **************************************************

	private ITicketSubjectRepository _ticketSubjectRepository;

	public ITicketSubjectRepository TicketSubjectRepository
	{
		get
		{
			_ticketSubjectRepository ??= new TicketSubjectRepository(DatabaseContext);
			return _ticketSubjectRepository;
		}
	}
// **************************************************
}