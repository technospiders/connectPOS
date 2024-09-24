using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Category.Commands
{
    public class DeleteVehicleTypeCommand
         : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
