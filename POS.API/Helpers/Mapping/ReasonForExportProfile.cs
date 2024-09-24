using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Category.Commands;

namespace POS.API.Helpers.Mapping
{
    /// <summary>
    /// ReasonForExport Mapper for Autommaper
    /// </summary>
    public class ReasonForExportProfile : Profile
    {
        public ReasonForExportProfile()
        {
            CreateMap<ReasonForExport, ReasonForExportDto>().ReverseMap();
            CreateMap<AddReasonForExportCommand, ReasonForExport>();
            CreateMap<UpdateReasonForExportCommand, ReasonForExport>();
        }
    }
}
