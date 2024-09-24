using AutoMapper;
using POS.Data.Dto;
using POS.Data;
using POS.MediatR.CommandAndQuery;
using POS.MediatR.PurchaseOrder.Commands;
using POS.MediatR.SalesOrder.Commands;
using POS.MediatR.SalesOrderPayment.Command;
using POS.Data.Entities;

namespace POS.API.Helpers.Mapping
{
    public class SalesOrderProfile : Profile
    {
        public SalesOrderProfile()
        {
            CreateMap<SalesOrder, SalesOrderDto>().ReverseMap();
            CreateMap<UpdateSalesOrderReturnCommand, SalesOrder>();
            CreateMap<AddSalesOrderCommand, SalesOrder>();
            CreateMap<SalesOrderItem, SalesOrderItemDto>().ReverseMap();
            CreateMap<SalesOrderItemTax, SalesOrderItemTaxDto>().ReverseMap();
            CreateMap<UpdateSalesOrderCommand, SalesOrder>();
            CreateMap<SalesOrderPayment,SalesOrderPaymentDto>().ReverseMap();
            CreateMap<AddSalesOrderPaymentCommand, SalesOrderPayment>();



            CreateMap<SaleOrderDetail, SaleOrderDetailDto>().ReverseMap();
            CreateMap<SalesOrder, SalesOrderDto>()
                .ForMember(dest => dest.LogisticsSaleOrderDetail, opt => opt.MapFrom(src => src.LogisticsSaleOrderDetail))
                .ReverseMap();

            // Mapping for SalesOrder including the SaleOrderDetail
            CreateMap<SaleOrderDetail, SaleOrderDetailDto>()
                .ForMember(dest => dest.LogisticsSaleOrderProductsItems, opt => opt.MapFrom(src => src.LogisticsSaleOrderProductsItems))
                .ReverseMap();
        }
    }
}
