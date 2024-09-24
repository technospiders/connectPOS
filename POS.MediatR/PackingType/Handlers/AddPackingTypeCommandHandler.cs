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
    public class AddPackingTypeCommandHandler
       : IRequestHandler<AddPackingTypeCommand, ServiceResponse<PackingTypeDto>>
    {
        private readonly IPackingTypeRepository _packingTypeRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPackingTypeCommandHandler> _logger;
        public AddPackingTypeCommandHandler(
           IPackingTypeRepository packingTypeRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<AddPackingTypeCommandHandler> logger
            )
        {
            _packingTypeRepository = packingTypeRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<PackingTypeDto>> Handle(AddPackingTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _packingTypeRepository
                .All
                .FirstOrDefaultAsync(c => c.Name == request.Name);

            if (existingEntity != null)
            {
                _logger.LogError("Packing Type Already Exist");
                return ServiceResponse<PackingTypeDto>.Return409("Packing Type Already Exist.");
            }
            var entity = _mapper.Map<PackingType>(request);
            entity.Id = Guid.NewGuid();
            _packingTypeRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving IPacking Type.");
                return ServiceResponse<PackingTypeDto>.Return500();
            }
            return ServiceResponse<PackingTypeDto>.ReturnResultWith200(_mapper.Map<PackingTypeDto>(entity));
        }
    }
}
