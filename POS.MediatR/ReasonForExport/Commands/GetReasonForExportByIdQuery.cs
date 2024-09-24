using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.MediatR.Category.Commands
{
    public class GetReasonForExportByIdQuery : IRequest<ServiceResponse<ReasonForExportDto>>
    {
        public Guid Id { get; set; }
    }
}
