using AutoMapper;
using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;

namespace AuthApi.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<GroceriesListDtos, GroceriesList>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<GroceriesList, GroceriesListDtos>();

            CreateMap<ListCategoryDtos, ListCategory>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ListCategory, ListCategoryDtos>();

            CreateMap<ListItemDtos, ListItem>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ListItem, ListItemDtos>();

            CreateMap<Role, ApplicationRole>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ApplicationRole, Role>();

        }
    }
}
