using AutoMapper;
using DataAccess.DTO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateCustomer, Customer>().ReverseMap();

            CreateMap<GetOrderbyCustomer, Customer>().ReverseMap();

            CreateMap<CreateOrder, Order>().ReverseMap();
            CreateMap<GetOrder, Order>().ReverseMap();
            CreateMap<GetAllOrders, Order>().ReverseMap();
            CreateMap<Status, Order>().ReverseMap();

            CreateMap<GetAllProducts, Product>().ReverseMap();
            CreateMap<GetProduct, Product>().ReverseMap();
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
            CreateMap<GetProduct,GetProductDto>().ReverseMap();

            CreateMap<GetInvoiceData,  Invoice>().ReverseMap();
            CreateMap<InvoiceDto, Invoice>().ReverseMap();

            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
        }
    }
}