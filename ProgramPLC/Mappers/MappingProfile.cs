using AutoMapper;
using ProgramPLC.DTOs;
using ProgramPLC.Models;
namespace ProgramPLC.Mappers
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {
            //CreateMap<Device, DeviceCreateDTO>().ReverseMap();
            CreateMap<Device, DeviceCreateDTO>();
            CreateMap<InitMonitorCreateDTO, InitMonitor>();
            //CreateMap<InitMonitor, InitMonitorReadDTO>().ForMember(dto => dto.InitMonitorId,
            //      m => m.MapFrom(b => b.InitMonitorId));
            //CreateMap<InitMonitorReadDTO, InitMonitor>();
        }
    }
}
