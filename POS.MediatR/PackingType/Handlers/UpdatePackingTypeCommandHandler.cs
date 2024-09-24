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
    public class UpdatePackingTypeCommandHandler
     : IRequestHandler<UpdatePackingTypeCommand, ServiceResponse<PackingTypeDto>>
    {
        private readonly IPackingTypeRepository _packingTypeRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePackingTypeCommandHandler> _logger;
        public UpdatePackingTypeCommandHandler(
           IPackingTypeRepository packingTypeRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdatePackingTypeCommandHandler> logger
            )
        {
            _packingTypeRepository = packingTypeRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<PackingTypeDto>> Handle(UpdatePackingTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _packingTypeRepository
                .All
                .FirstOrDefaultAsync(c => c.Name == request.Name
                && c.Id != request.Id);
            if (existingEntity != null)
            {
                _logger.LogError("Packing Type Already Exist");
                return ServiceResponse<PackingTypeDto>.Return409("Packing Type Already Exist.");
            }

            existingEntity = await _packingTypeRepository.FindAsync(request.Id);
            var entity = _mapper.Map(request, existingEntity);
            _packingTypeRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Packing Type.");
                return ServiceResponse<PackingTypeDto>.Return500();
            }
            return ServiceResponse<PackingTypeDto>.ReturnResultWith200(_mapper.Map<PackingTypeDto>(entity));
        }
    }
}
