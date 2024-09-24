using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Category.Commands;

namespace POS.API.Helpers.Mapping
{
    /// <summary>
    /// PackingType Mapper for Autommaper
    /// </summary>
    public class VehicleTypeProfile : Profile
    {
        public VehicleTypeProfile()
        {
            CreateMap<VehicleType, VehicleTypeDto>().ReverseMap();
            CreateMap<AddVehicleTypeCommand, VehicleType>();
            CreateMap<UpdateVehicleTypeCommand, VehicleType>();
        }
    }
}
