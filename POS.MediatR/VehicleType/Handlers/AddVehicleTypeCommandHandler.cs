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
    public class AddVehicleTypeCommandHandler
       : IRequestHandler<AddVehicleTypeCommand, ServiceResponse<VehicleTypeDto>>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddVehicleTypeCommandHandler> _logger;
        public AddVehicleTypeCommandHandler(
           IVehicleTypeRepository vehicleTypeRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<AddVehicleTypeCommandHandler> logger
            )
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<VehicleTypeDto>> Handle(AddVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _vehicleTypeRepository
                .All
                .FirstOrDefaultAsync(c => c.VehicleTypeName == request.VehicleTypeName);

            if (existingEntity != null)
            {
                _logger.LogError("Vehicle Type Already Exist");
                return ServiceResponse<VehicleTypeDto>.Return409("Vehicle Type Already Exist.");
            }
            var entity = _mapper.Map<VehicleType>(request);
            entity.Id = Guid.NewGuid();
            _vehicleTypeRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving IVehicle Type.");
                return ServiceResponse<VehicleTypeDto>.Return500();
            }
            return ServiceResponse<VehicleTypeDto>.ReturnResultWith200(_mapper.Map<VehicleTypeDto>(entity));
        }
    }
}
