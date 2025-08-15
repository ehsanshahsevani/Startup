using Domain;
using ViewModels.Marketplace;
using ViewModels.ProjectManager;
using InfrastructureSeedworks.AutoMapper;

namespace Infrastructure.Profiles;

public class MappingProfile : BaseProfileAutoMapper
{
    public MappingProfile() : base()
    {
        // **************************************************
        CreateMap<Product, ProductResponseViewModel>()
            
            .ForMember(x => x.CategoryDisplayName,
                opt => opt.MapFrom(x => x.Category!.Name))

            .ForMember(x => x.ProductBranchResponseViewModels,
                opt => opt.MapFrom(x => x.ProductBranches))
            ;

        CreateMap<ProductRequestViewModel, Product>()
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
        CreateMap<ProductBranch, ProductBranchResponseViewModel>()
            
            .ForMember(x => x.BranchDisplayName,
                opt => opt.MapFrom(x => x.Branch!.Name))

            .ForMember(x => x.ProductDisplayName,
                opt => opt.MapFrom(x => x.Product!.Name))
            ;
        // **************************************************

        // **************************************************
        CreateMap<RoleGold, RoleGoldResponseViewModel>()

            .ForMember(x => x.TypeRoleGoldDisplayName,
                opt => opt.MapFrom(x => x.TypeRoleGold!.Name))
            ;

        CreateMap<RoleGoldRequestViewModel, RoleGold>()
            
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
        CreateMap<TalaSootSettings, TalaSootSettingsResponseViewModel>();
        
        CreateMap<TalaSootSettingsRequestViewModel, TalaSootSettings>()
            
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
        CreateMap<RoleMoney, RoleMoneyResponseViewModel>()

            .ForMember(x => x.TypeRoleMoneyDisplayName,
                opt => opt.MapFrom(x => x.TypeRoleMoney!.Name))
            ;

        CreateMap<RoleMoneyRequestViewModel, RoleMoney>()
            
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
        CreateMap<TypeRoleGold, TypeRoleGoldResponseViewModel>();

        CreateMap<TypeRoleGoldRequestViewModel, TypeRoleGold>()
            
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
        CreateMap<Branch, BranchResponseViewModel>()
            .ForMember(x => x.ProvinceId,
                opt => opt.MapFrom(x => x.City!.ProvinceId))
            
            .ForMember(x => x.ProvinceDisplayName,
                opt => { opt.MapFrom(dest => dest.City!.Province.Name); })
            
            .ForMember(x => x.CityDisplayName,
                opt => { opt.MapFrom(dest => dest.City!.Name); });

        CreateMap<BranchRequestViewModel, Branch>()
            
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
        CreateMap<CartItem, CartItemResponseViewModel>();

        CreateMap<CartItemRequestViewModel, CartItem>()
            
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
        CreateMap<Category, CategoryResponseViewModel>();

        CreateMap<CategoryRequestViewModel, Category>()
            
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
        CreateMap<Attachment, AttachmentResponseViewModel>()
            .ForMember(x => x.AttachmentSubjectDisplayName,
                opt => { opt.MapFrom(attachment => attachment.AttachmentSubject!.DisplayName); })
            ;

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
        CreateMap<TagPageSetting, TagPageSettingResponseViewModel>();
        
        CreateMap<PageSetting, PageSettingResponseViewModel>()
            
            .ForMember(dest => dest.TagNames,
                opt =>
                    opt.MapFrom(src => src.PageSettingTagPageSettings.Select(x => x.TagPageSetting!.NameEn)))
            
            ;
        
        CreateMap<PageSettingRequestViewModel, PageSetting>()
            
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            
            .ReverseMap();
        // ******
        CreateMap<PageSetting, FaqResponseViewModel>();
        CreateMap<FaqRequestViewModel, PageSetting>()
            
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            
            .ReverseMap();
        
        // ******
        CreateMap<PageSetting, BannerResponseViewModel>()

            .ForMember(dest => dest.TagNames,
                opt =>
                    opt.MapFrom(src => src.PageSettingTagPageSettings.Distinct().Select(x => x.TagPageSetting!.NameEn)))
            
            ;
        
        CreateMap<BannerRequestViewModel, PageSetting>()
            
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            
            .ReverseMap();
        
        // ******
        CreateMap<PageSetting, SocialResponseViewModel>();
        CreateMap<SocialRequestViewModel, PageSetting>()
            
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src =>
                        string.IsNullOrEmpty(src.Id) == true ? Guid.NewGuid().ToString() : src.Id))
            
            .ForMember(dest => dest.UpdateDateTime,
                opt =>
                    opt.MapFrom(src => DateTime.Now))
            
            .ReverseMap();
        
        // ******
        CreateMap<PageSetting, TextDynamicResponseViewModel>()
            
            .ForMember(dest => dest.TagNames,
                opt =>
                    opt.MapFrom(src => src.PageSettingTagPageSettings.Distinct().Select(x => x.TagPageSetting!.NameEn)))
            
            ;
        
        CreateMap<TextDynamicRequestViewModel, PageSetting>()
            
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
        CreateMap<Shop, ShopResponseViewModel>()
            .ReverseMap();

        CreateMap<ShopRequestViewModel, Shop>()
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