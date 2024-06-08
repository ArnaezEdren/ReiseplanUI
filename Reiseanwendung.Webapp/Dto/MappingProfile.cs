using AutoMapper;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;

namespace Reiseanwendung.Webapp.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TravelplanDto, TravelPlan>();
            CreateMap<TravelPlan, TravelplanDto>();
        
        }
    }
}
