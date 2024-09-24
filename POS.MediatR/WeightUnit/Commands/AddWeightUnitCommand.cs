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
    public class AddWeightUnitCommand 
        : IRequest<ServiceResponse<WeightUnitDto>>
    {
        public string UnitName { get; set; }
        public bool IsStatus { get; set; }

    }
}
