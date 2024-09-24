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
    public class AddReasonForExportCommandHandler
       : IRequestHandler<AddReasonForExportCommand, ServiceResponse<ReasonForExportDto>>
    {
        private readonly IReasonForExportRepository _reasonForExportRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddReasonForExportCommandHandler> _logger;
        public AddReasonForExportCommandHandler(
           IReasonForExportRepository reasonForExportRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<AddReasonForExportCommandHandler> logger
            )
        {
            _reasonForExportRepository = reasonForExportRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<ReasonForExportDto>> Handle(AddReasonForExportCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _reasonForExportRepository
                .All
                .FirstOrDefaultAsync(c => c.Name == request.Name);

            if (existingEntity != null)
            {
                _logger.LogError("Reason For Export Already Exist");
                return ServiceResponse<ReasonForExportDto>.Return409("Reason For Export Already Exist.");
            }
            var entity = _mapper.Map<ReasonForExport>(request);
            entity.Id = Guid.NewGuid();
            _reasonForExportRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving IReason For Export.");
                return ServiceResponse<ReasonForExportDto>.Return500();
            }
            return ServiceResponse<ReasonForExportDto>.ReturnResultWith200(_mapper.Map<ReasonForExportDto>(entity));
        }
    }
}
