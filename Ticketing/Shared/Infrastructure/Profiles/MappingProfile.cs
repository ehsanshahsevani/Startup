using Domain;
using InfrastructureSeedworks.AutoMapper;
using ViewModels.Marketplace;
using ViewModels.ProjectManager;
using ViewModels.Ticketing;

namespace Infrastructure.Profiles;

public class MappingProfile : BaseProfileAutoMapper
{
    public MappingProfile()
    {
        // **************************************************
        CreateMap<Ticket, TicketResponseViewModel>()
            .ForMember(x => x.TicketSubjectDisplayName,
                opt => opt.MapFrom(x => x.TicketSubject!.Name))
            .ForMember(x => x.StatusDisplayName,
                opt => opt.MapFrom(x => x.Status!.Name))
            .ForMember(x => x.TicketMessageResponseViewModels,
                opt => opt.MapFrom(x => x.TicketMessages))
        ;

        CreateMap<TicketRequestViewModel, Ticket>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            .ReverseMap();
        // **************************************************

        // **************************************************
        CreateMap<TicketMessage, TicketMessageResponseViewModel>();

        CreateMap<TicketMessageRequestViewModel, TicketMessage>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            .ReverseMap();
        // **************************************************


        // **************************************************
        CreateMap<SubSystemLocal, SubSystemResponseViewModel>()
            .ReverseMap();

        CreateMap<SubSystemResponseViewModel, SubSystemLocal>()
            .ReverseMap();
        // **************************************************

        // **************************************************
        CreateMap<Attachment, AttachmentResponseViewModel>()
            .ForMember(x => x.AttachmentSubjectDisplayName,
                opt => { opt.MapFrom(attachment => attachment.AttachmentSubject!.DisplayName); });
        // **************************************************

        // **************************************************
        CreateMap<AttachmentRequestViewModel, Attachment>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            .ReverseMap();
        // **************************************************

        // **************************************************
        CreateMap<SubSystemLocal, SubSystemLocalResponseViewModel>();
        // **************************************************
    }
}