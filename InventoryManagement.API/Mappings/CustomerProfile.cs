using AutoMapper;
using InventoryManagement.API.DTOs;
using InventoryManagement.API.Models;

namespace InventoryManagement.API.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // Entity -> DTO
        CreateMap<Customer, CustomerDto>();

        // DTO -> Entity
        CreateMap<CreateCustomerDto, Customer>();

        // DTO -> Entity
        CreateMap<UpdateCustomerDto, Customer>();
    }
}