using AutoMapper;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using POS.MediatR.Category.Commands;

namespace POS.MediatR.Category.Handlers
{
    public class UpdateVehicleTypeCommandHandler
     : IRequestHandler<UpdateVehicleTypeCommand, ServiceResponse<VehicleTypeDto>>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVehicleTypeCommandHandler> _logger;
        public UpdateVehicleTypeCommandHandler(
           IVehicleTypeRepository vehicleTypeRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateVehicleTypeCommandHandler> logger
            )
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<VehicleTypeDto>> Handle(UpdateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _vehicleTypeRepository
                .All
                .FirstOrDefaultAsync(c => c.VehicleTypeName == request.VehicleTypeName
                && c.Id != request.Id);
            if (existingEntity != null)
            {
                _logger.LogError("Vehicle Type Already Exist");
                return ServiceResponse<VehicleTypeDto>.Return409("Vehicle Type Already Exist.");
            }

            existingEntity = await _vehicleTypeRepository.FindAsync(request.Id);
            var entity = _mapper.Map(request, existingEntity);
            _vehicleTypeRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Vehicle Type.");
                return ServiceResponse<VehicleTypeDto>.Return500();
            }
            return ServiceResponse<VehicleTypeDto>.ReturnResultWith200(_mapper.Map<VehicleTypeDto>(entity));
        }
    }
}
