using AutoMapper;
using SIOMS.DTOs;
using SIOMS.Models;

namespace SIOMS.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponseDto>();
        CreateMap<CreateProductDto, Product>();
    }
}