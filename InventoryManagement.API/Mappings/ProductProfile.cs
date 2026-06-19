using AutoMapper;
using InventoryManagement.API.DTOs;
using InventoryManagement.API.Models;

namespace InventoryManagement.API.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductDto, Product>();

        CreateMap<UpdateProductDto, Product>();
    }
}