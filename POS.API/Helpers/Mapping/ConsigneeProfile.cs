using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.CommandAndQuery;

namespace POS.API.Helpers
{
    /// <summary>
    /// Consignee Mapper for Autommaper
    /// </summary>
    public class ConsigneeProfile : Profile
    {
        /// <summary>
        /// Supplier Mapper for Autommaper
        /// </summary>
        public ConsigneeProfile()
        {
            CreateMap<ConsigneeAddressDto, ConsigneeAddress>().ReverseMap();
            CreateMap<Consignee, ConsigneeDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description == null ? string.Empty : src.Description)).ReverseMap();
            CreateMap<AddConsigneeCommand, Consignee>();
            CreateMap<UpdateConsigneeCommand, Consignee>();
        }
    }
}
