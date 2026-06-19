using AutoMapper;
using InventoryManagement.API.DTOs;
using InventoryManagement.API.Models;

namespace InventoryManagement.API.Mappings;

public class InvoiceItemProfile : Profile
{
    public InvoiceItemProfile()
    {
        CreateMap<InvoiceItem, InvoiceItemDto>();

        CreateMap<CreateInvoiceItemDto, InvoiceItem>();

        CreateMap<UpdateInvoiceItemDto, InvoiceItem>();
    }
}