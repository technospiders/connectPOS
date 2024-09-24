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
    public class UpdateReasonForExportCommandHandler
     : IRequestHandler<UpdateReasonForExportCommand, ServiceResponse<ReasonForExportDto>>
    {
        private readonly IReasonForExportRepository _reasonForExportRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateReasonForExportCommandHandler> _logger;
        public UpdateReasonForExportCommandHandler(
           IReasonForExportRepository reasonForExportRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateReasonForExportCommandHandler> logger
            )
        {
            _reasonForExportRepository = reasonForExportRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<ReasonForExportDto>> Handle(UpdateReasonForExportCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _reasonForExportRepository
                .All
                .FirstOrDefaultAsync(c => c.Name == request.Name
                && c.Id != request.Id);
            if (existingEntity != null)
            {
                _logger.LogError("Reason For Export Already Exist");
                return ServiceResponse<ReasonForExportDto>.Return409("Reason For Export Already Exist.");
            }

            existingEntity = await _reasonForExportRepository.FindAsync(request.Id);
            var entity = _mapper.Map(request, existingEntity);
            _reasonForExportRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Reason For Export.");
                return ServiceResponse<ReasonForExportDto>.Return500();
            }
            return ServiceResponse<ReasonForExportDto>.ReturnResultWith200(_mapper.Map<ReasonForExportDto>(entity));
        }
    }
}
