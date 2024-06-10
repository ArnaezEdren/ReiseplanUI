using AutoMapper;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto;
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
        CreateMap<PersonDto, Person>(); 

        CreateMap<TravelPlan, TravelplanDto>();

        CreateMap<Destination, DestinationDto>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<Accommodation, AccommodationDto>();
        CreateMap<Transportation, TransportationDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Person, PersonDto>(); 
    }
}