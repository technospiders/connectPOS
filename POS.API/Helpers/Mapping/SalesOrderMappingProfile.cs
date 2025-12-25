using AutoMapper;
using POS.Data.Dto;
using POS.Data.Entities;
using POS.Data;
using POS.MediatR.CommandAndQuery;
using POS.MediatR.SalesOrder.Commands;

namespace POS.API.Helpers.Mapping
{
    public class SalesOrderMappingProfile : Profile
    {
        public SalesOrderMappingProfile()
        {
            // Mapping AddSalesOrderCommand to SalesOrder entity
            CreateMap<AddSalesOrderCommand, SalesOrder>()
                .ForMember(dest => dest.LogisticsSaleOrderDetail, opt => opt.MapFrom(src => src.LogisticsSaleOrderDetail))
                //.ForMember(dest => dest.LogisticsSaleOrderDetail.LogisticsSaleOrderProductsItems, opt => opt.MapFrom(src => src.LogisticsSaleOrderProductsItems))
                .ForMember(dest => dest.SalesOrderItems, opt => opt.MapFrom(src => src.SalesOrderItems));

            // Mapping UpdateSalesOrderCommand to SalesOrder entity
            CreateMap<UpdateSalesOrderCommand, SalesOrder>()
                .ForMember(dest => dest.LogisticsSaleOrderDetail, opt => opt.MapFrom(src => src.LogisticsSaleOrderDetail)) // Mapping SaleOrderDetail
                .ForMember(dest => dest.SalesOrderItems, opt => opt.MapFrom(src => src.SalesOrderItems));

            // Mapping SaleOrderDetailDto to SaleOrderDetail entity
            CreateMap<SaleOrderDetailDto, SaleOrderDetail>()
                .ForMember(dest => dest.LogisticsSaleOrderProductsItems, opt => opt.MapFrom(src => src.LogisticsSaleOrderProductsItems)); // Mapping nested products


            // Mapping SaleOrderProductsItemsDto to SaleOrderProductsItems entity
            CreateMap<SaleOrderProductsItemsDto, SaleOrderProductsItems>();

            // Mapping SalesOrderItemDto to SalesOrderItem entity
            CreateMap<SalesOrderItemDto, SalesOrderItem>();
        }
    }
}
