using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Command, CommandDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.City, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Player, PlayerDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CommandForCreationDto, Command>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<PlayerForCreationDto, Player>();
        }
    }
}
