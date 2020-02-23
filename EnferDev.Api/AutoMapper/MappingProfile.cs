using AutoMapper;
using EnferDev.Api.ViewModels;
using EnferDev.Domain.Entities;

namespace EnferDev.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hospital, HospitalViewModel>()
                .ForMember(h => h.CNPJ, c => c.MapFrom(m => m.CNPJ.Number))
                .ForMember(h => h.IdAddress, c => c.MapFrom(m => m.IdAddress))
                .ForMember(h => h.Street, c => c.MapFrom(m => m.Address.Street))
                .ForMember(h => h.Number, c => c.MapFrom(m => m.Address.Number))
                .ForMember(h => h.City, c => c.MapFrom(m => m.Address.City))
                .ForMember(h => h.State, c => c.MapFrom(m => m.Address.State))
                .ForMember(h => h.Country, c => c.MapFrom(m => m.Address.Country))
                .ForMember(h => h.ZipCode, c => c.MapFrom(m => m.Address.ZipCode));

            CreateMap<Nurse, NurseViewModel>()
                .ForMember(n => n.CPF, c => c.MapFrom(m => m.CPF.Number));

            CreateMap<Address, AddressViewModel>();
        }
    }
}
