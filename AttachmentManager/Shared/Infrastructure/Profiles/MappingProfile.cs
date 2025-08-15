using Domain;
using InfrastructureSeedworks.AutoMapper;
using ViewModels.ProjectManager;

namespace Infrastructure.Profiles;

public class MappingProfile : BaseProfileAutoMapper
{
	public MappingProfile() : base()
	{
		// **************************************************
		CreateMap<SubSystemResponseViewModel, SubSystemLocal>()
		    .ReverseMap();
		// **************************************************
	}
}
