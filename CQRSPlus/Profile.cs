using AutoMapper;
using CQRSPlus.Entities.Models;
using CQRSPlus.Shared.DataTransferObjects;

namespace CQRSPlus
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForCtorParam("FullAddress",opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        }
    }

}
