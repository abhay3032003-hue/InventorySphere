using AutoMapper;
using InventoryManagement.API.DTOs;
using InventoryManagement.API.Models;

namespace InventoryManagement.API.Mappings;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>();

        CreateMap<CreateInvoiceDto, Invoice>();

        CreateMap<UpdateInvoiceDto, Invoice>();
    }
}