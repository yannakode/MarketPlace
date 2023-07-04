using AutoMapper;
using MarketPlace.Models;
using MarketPlace.Models.DTO;
using MarketPlace.Repositories.Abstract;

namespace MarketPlace.Mapping
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
