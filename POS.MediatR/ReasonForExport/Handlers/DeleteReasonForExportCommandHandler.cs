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
using System.Linq;

namespace POS.MediatR.Category.Handlers
{
    public class DeleteReasonForExportCommandHandler
        : IRequestHandler<DeleteReasonForExportCommand, ServiceResponse<bool>>
    {
        private readonly IReasonForExportRepository _reasonForExportRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteReasonForExportCommandHandler> _logger;
        public DeleteReasonForExportCommandHandler(
           IReasonForExportRepository reasonForExportRepository,
           IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteReasonForExportCommandHandler> logger
            )
        {
            _reasonForExportRepository = reasonForExportRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteReasonForExportCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _reasonForExportRepository
                .FindAsync(request.Id);
           
            if (existingEntity == null)
            {
                _logger.LogError("Reason For Export not Exists");
                return ServiceResponse<bool>.Return409("Reason For Export not Exists.");
            }

            var exitingProduct = await _productRepository.AllIncluding().AnyAsync(c => c.CategoryId == existingEntity.Id);
            if (exitingProduct)
            {
                _logger.LogError("Reason For Export not Deleted because it is use in product");
                return ServiceResponse<bool>.Return409("Reason For Export not Deleted because it is use in product.");
            }

            _reasonForExportRepository.Delete(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Reason For Export.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
