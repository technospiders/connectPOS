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
    public class AddPackingTypeCommand 
        : IRequest<ServiceResponse<PackingTypeDto>>
    {
        public string Name { get; set; }
    }
}
