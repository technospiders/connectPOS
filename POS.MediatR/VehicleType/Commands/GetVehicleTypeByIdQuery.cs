using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Category.Commands
{
    public class GetVehicleTypeByIdQuery : IRequest<ServiceResponse<VehicleTypeDto>>
    {
        public Guid Id { get; set; }
    }
}
