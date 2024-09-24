using MediatR;
using POS.Data.Dto;
using POS.Helper;

namespace POS.MediatR.Category.Commands
{
    public class AddVehicleTypeCommand 
        : IRequest<ServiceResponse<VehicleTypeDto>>
    {
        public string VehicleTypeName { get; set; }
        public bool IsStatus { get; set; }

    }
}
