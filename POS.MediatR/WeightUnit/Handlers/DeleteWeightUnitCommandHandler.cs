using AutoMapper;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using POS.MediatR.Category.Commands;
namespace POS.MediatR.Category.Handlers
{
    public class DeleteWeightUnitCommandHandler
        : IRequestHandler<DeleteWeightUnitCommand, ServiceResponse<bool>>
    {
        private readonly IWeightUnitRepository _weightUnitRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteWeightUnitCommandHandler> _logger;
        public DeleteWeightUnitCommandHandler(
           IWeightUnitRepository weightUnitRepository,
           IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteWeightUnitCommandHandler> logger
            )
        {
            _weightUnitRepository = weightUnitRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteWeightUnitCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _weightUnitRepository
                .FindAsync(request.Id);
           
            if (existingEntity == null)
            {
                _logger.LogError("Weight Unit not Exists");
                return ServiceResponse<bool>.Return409("Weight Unit not Exists.");
            }

            var exitingProduct = await _productRepository.AllIncluding().AnyAsync(c => c.CategoryId == existingEntity.Id);
            if (exitingProduct)
            {
                _logger.LogError("Weight Unit not Deleted because it is use in product");
                return ServiceResponse<bool>.Return409("Weight Unit not Deleted because it is use in product.");
            }

            _weightUnitRepository.Delete(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Weight Unit.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
