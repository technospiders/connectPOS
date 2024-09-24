using AutoMapper;
using MediatR;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Category.Commands;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Category.Handlers
{
    public class GetVehicleTypeByIdQueryHandler
        : IRequestHandler<GetVehicleTypeByIdQuery, ServiceResponse<VehicleTypeDto>>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IMapper _mapper;

        public GetVehicleTypeByIdQueryHandler(
            IVehicleTypeRepository vehicleTypeRepository,
            IMapper mapper)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<VehicleTypeDto>> Handle(GetVehicleTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _vehicleTypeRepository.FindAsync(request.Id);
            if (category == null)
            {
                return ServiceResponse<VehicleTypeDto>.Return404();
            }

            return ServiceResponse<VehicleTypeDto>.ReturnResultWith200(_mapper.Map<VehicleTypeDto>(category));
        }
    }
}
