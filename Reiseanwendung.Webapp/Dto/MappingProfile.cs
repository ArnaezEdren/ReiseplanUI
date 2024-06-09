using AutoMapper;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TravelplanDto, TravelPlan>();
        CreateMap<DestinationDto, Destination>();
        CreateMap<ActivityDto, Activity>();
        CreateMap<AccommodationDto, Accommodation>();
        CreateMap<TransportationDto, Transportation>();
        CreateMap<AddressDto, Address>();
        CreateMap<PersonDto, Person>(); // Ensure this mapping exists
    }
}
