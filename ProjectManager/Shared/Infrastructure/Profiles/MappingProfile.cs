using Domain;
using InfrastructureSeedworks.AutoMapper;
using ViewModels.ProjectManager;
using ViewModels.Shared;
using Action = Domain.Action;

namespace Infrastructure.Profiles;

public class MappingProfile : BaseProfileAutoMapper
{
	public MappingProfile() : base()
	{
		// **************************************************
		CreateMap<UserRelation, UserRelationTemp>()
			
			.ReverseMap();

		CreateMap<UserRelationTemp, UserRelationResponseViewModel>()
			.ReverseMap()
			.ForMember(dest => dest.Id,
				opt =>
					opt.MapFrom(src =>
						string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
			;

		CreateMap<UserRelation, UserRelationResponseViewModel>()
			
			.ReverseMap()
			
			;
		// **************************************************
		
		// **************************************************
		CreateMap<SubSystem, SubSystemResponseViewModel>()
			
			.ReverseMap()
			
			;
		// **************************************************

		// **************************************************
		CreateMap<Action, ActionViewModel>()
			
			.ReverseMap()
			
			;
		// **************************************************
	}
}
