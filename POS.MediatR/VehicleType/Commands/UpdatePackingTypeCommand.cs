using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Category.Commands
{
    public class UpdateVehicleTypeCommand
          : IRequest<ServiceResponse<VehicleTypeDto>>
    {
        public Guid Id { get; set; }
        public string VehicleTypeName { get; set; }
        public bool IsStatus { get; set; }
    }
}
