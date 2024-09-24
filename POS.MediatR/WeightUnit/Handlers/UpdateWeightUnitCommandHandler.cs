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
    public class UpdateWeightUnitCommandHandler
     : IRequestHandler<UpdateWeightUnitCommand, ServiceResponse<WeightUnitDto>>
    {
        private readonly IWeightUnitRepository _weightUnitRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWeightUnitCommandHandler> _logger;
        public UpdateWeightUnitCommandHandler(
           IWeightUnitRepository weightUnitRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateWeightUnitCommandHandler> logger
            )
        {
            _weightUnitRepository = weightUnitRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<WeightUnitDto>> Handle(UpdateWeightUnitCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _weightUnitRepository
                .All
                .FirstOrDefaultAsync(c => c.UnitName == request.UnitName
                && c.Id != request.Id);
            if (existingEntity != null)
            {
                _logger.LogError("Weight Unit Already Exist");
                return ServiceResponse<WeightUnitDto>.Return409("Weight Unit Already Exist.");
            }

            existingEntity = await _weightUnitRepository.FindAsync(request.Id);
            var entity = _mapper.Map(request, existingEntity);
            _weightUnitRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Weight Unit.");
                return ServiceResponse<WeightUnitDto>.Return500();
            }
            return ServiceResponse<WeightUnitDto>.ReturnResultWith200(_mapper.Map<WeightUnitDto>(entity));
        }
    }
}
