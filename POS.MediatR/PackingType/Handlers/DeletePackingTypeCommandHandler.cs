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
    public class DeletePackingTypeCommandHandler
        : IRequestHandler<DeletePackingTypeCommand, ServiceResponse<bool>>
    {
        private readonly IPackingTypeRepository _packingTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePackingTypeCommandHandler> _logger;
        public DeletePackingTypeCommandHandler(
           IPackingTypeRepository packingTypeRepository,
           IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeletePackingTypeCommandHandler> logger
            )
        {
            _packingTypeRepository = packingTypeRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeletePackingTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _packingTypeRepository
                .FindAsync(request.Id);
           
            if (existingEntity == null)
            {
                _logger.LogError("Packing Type not Exists");
                return ServiceResponse<bool>.Return409("Packing Type not Exists.");
            }

            var exitingProduct = await _productRepository.AllIncluding().AnyAsync(c => c.CategoryId == existingEntity.Id);
            if (exitingProduct)
            {
                _logger.LogError("Packing Type not Deleted because it is use in product");
                return ServiceResponse<bool>.Return409("Packing Type not Deleted because it is use in product.");
            }

            _packingTypeRepository.Delete(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Packing Type.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
