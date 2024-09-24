using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Category.Commands;

namespace POS.API.Helpers.Mapping
{
    /// <summary>
    /// WeightUnit Mapper for Autommaper
    /// </summary>
    public class WeightUnitProfile : Profile
    {
        public WeightUnitProfile()
        {
            CreateMap<WeightUnit, WeightUnitDto>().ReverseMap();
            CreateMap<AddWeightUnitCommand, WeightUnit>();
            CreateMap<UpdateWeightUnitCommand, WeightUnit>();
        }
    }
}
