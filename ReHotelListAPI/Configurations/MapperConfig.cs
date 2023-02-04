using AutoMapper;
using ReHotelListAPI.Data;
using ReHotelListAPI.Models.Country;
using ReHotelListAPI.Models.Hotel;

namespace ReHotelListAPI.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
           
            
            
            CreateMap<Hotel, HotelDto>().ReverseMap();


        }
    }
}
