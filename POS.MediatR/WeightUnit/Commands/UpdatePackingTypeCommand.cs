using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Category.Commands
{
    public class UpdateWeightUnitCommand
          : IRequest<ServiceResponse<WeightUnitDto>>
    {
        public Guid Id { get; set; }
        public string UnitName { get; set; }
        public bool IsStatus { get; set; }
    }
}
