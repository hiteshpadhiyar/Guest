using AutoMapper;
using Guest.Entities;
using Guest.Models;

namespace Guest.Mappings
{
    public class GuestMapperProfile : Profile
    {
        public GuestMapperProfile()
        {
            CreateMap<Guests, GuestsItem>()
        .ForMember(dest =>
            dest.Id,
            opt => opt.MapFrom(src => src.Id))
        .ForMember(dest =>
            dest.FirstName,
            opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest =>
            dest.LastName,
            opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest =>
            dest.Email,
            opt => opt.MapFrom(src => src.Email))
        .ForMember(dest =>
            dest.BirthDate,
            opt => opt.MapFrom(src => src.BirthDate))
        .AfterMap(
                (src, dest) => dest.Phone_Numbers.AddRange(
                    src.PhoneNumbers.Split(',').ToList())
                );
        }
    }
}