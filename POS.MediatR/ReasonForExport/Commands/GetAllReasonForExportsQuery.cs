using POS.Data.Dto;
using MediatR;
using System.Collections.Generic;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllReasonForExportsQuery : IRequest<List<ReasonForExportDto>>
    {
        public Guid? Id { get; set; }
        public bool IsDropDown { get; set; } = true;
    }
}
