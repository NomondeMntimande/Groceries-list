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
           
        }
    }
}
