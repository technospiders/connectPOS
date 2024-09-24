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
    public class AddWeightUnitCommandHandler
       : IRequestHandler<AddWeightUnitCommand, ServiceResponse<WeightUnitDto>>
    {
        private readonly IWeightUnitRepository _weightUnitRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddWeightUnitCommandHandler> _logger;
        public AddWeightUnitCommandHandler(
           IWeightUnitRepository weightUnitRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<AddWeightUnitCommandHandler> logger
            )
        {
            _weightUnitRepository = weightUnitRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<WeightUnitDto>> Handle(AddWeightUnitCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _weightUnitRepository
                .All
                .FirstOrDefaultAsync(c => c.UnitName == request.UnitName);

            if (existingEntity != null)
            {
                _logger.LogError("Weight Unit Already Exist");
                return ServiceResponse<WeightUnitDto>.Return409("Weight Unit Already Exist.");
            }
            var entity = _mapper.Map<WeightUnit>(request);
            entity.Id = Guid.NewGuid();
            _weightUnitRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving IWeight Unit.");
                return ServiceResponse<WeightUnitDto>.Return500();
            }
            return ServiceResponse<WeightUnitDto>.ReturnResultWith200(_mapper.Map<WeightUnitDto>(entity));
        }
    }
}
