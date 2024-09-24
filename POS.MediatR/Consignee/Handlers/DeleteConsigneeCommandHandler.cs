using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace POS.MediatR.Handlers
{
    public class DeleteConsigneeCommandHandler : IRequestHandler<DeleteConsigneeCommand, ServiceResponse<bool>>
    {
        private readonly IConsigneeRepository _consigneeRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<AddConsigneeCommandHandler> _logger;
        public DeleteConsigneeCommandHandler(
            IConsigneeRepository consigneeRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            ILogger<AddConsigneeCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow)
        {
            _consigneeRepository = consigneeRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteConsigneeCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _consigneeRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Consignee is not exist");
                return ServiceResponse<bool>.Return422("Consignee does not exist");
            }

            //Commented this code because consignee can't used in purchase order

            //var exitingConsignee = _purchaseOrderRepository.All.Any(c => c.ConsigneeId == entityExist.Id);
            //if (exitingConsignee)
            //{
            //    _logger.LogError("Consignee can not be Deleted because it is use in Purchase Order");
            //    return ServiceResponse<bool>.Return409("Consignee can not be Deleted because it is use in Purchase Order");
            //}

            _consigneeRepository.Delete(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Delete Consignee");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
