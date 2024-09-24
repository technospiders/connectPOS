using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Category.Commands;

namespace POS.API.Helpers.Mapping
{
    /// <summary>
    /// PackingType Mapper for Autommaper
    /// </summary>
    public class PackingTypeProfile : Profile
    {
        public PackingTypeProfile()
        {
            CreateMap<PackingType, PackingTypeDto>().ReverseMap();
            CreateMap<AddPackingTypeCommand, PackingType>();
            CreateMap<UpdatePackingTypeCommand, PackingType>();
        }
    }
}
