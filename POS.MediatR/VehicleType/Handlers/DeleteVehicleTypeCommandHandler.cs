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
    public class DeleteVehicleTypeCommandHandler
        : IRequestHandler<DeleteVehicleTypeCommand, ServiceResponse<bool>>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteVehicleTypeCommandHandler> _logger;
        public DeleteVehicleTypeCommandHandler(
           IVehicleTypeRepository vehicleTypeRepository,
           IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteVehicleTypeCommandHandler> logger
            )
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _vehicleTypeRepository
                .FindAsync(request.Id);
           
            if (existingEntity == null)
            {
                _logger.LogError("Vehicle Type not Exists");
                return ServiceResponse<bool>.Return409("Vehicle Type not Exists.");
            }

            var exitingProduct = await _productRepository.AllIncluding().AnyAsync(c => c.CategoryId == existingEntity.Id);
            if (exitingProduct)
            {
                _logger.LogError("Vehicle Type not Deleted because it is use in product");
                return ServiceResponse<bool>.Return409("Vehicle Type not Deleted because it is use in product.");
            }

            _vehicleTypeRepository.Delete(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Vehicle Type.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
