using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetSalesOrderQuaryHandler : IRequestHandler<GetSalesOrderCommand, ServiceResponse<SalesOrderDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;

        public GetSalesOrderQuaryHandler(ISalesOrderRepository salesOrderRepository,
            IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<SalesOrderDto>> Handle(GetSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _salesOrderRepository.All
                 .Include(c => c.SalesOrderPayments)
                 .Include(c => c.Customer)
                 .Include(c => c.SalesOrderItems)
                     .ThenInclude(c => c.SalesOrderItemTaxes)
                     .ThenInclude(cs => cs.Tax)
                .Include(c => c.SalesOrderItems)
                    .ThenInclude(c => c.Product)
                .Include(c => c.SalesOrderItems)
                    .ThenInclude(c => c.UnitConversation)
                // Include the Logistics-specific details
                .Include(c => c.LogisticsSaleOrderDetail) // Include SaleOrderDetail
                    .ThenInclude(l => l.Consignee) // Include the Consignee in SaleOrderDetail
                    .ThenInclude(l => l.ConsigneeAddress) // Include the ConsigneeAddresses in Consignee
                .Include(c => c.LogisticsSaleOrderDetail)
                    .ThenInclude(l => l.PackingType) // Include PackingType
                //.Include(c => c.LogisticsSaleOrderDetail)
                //    .ThenInclude(l => l.WeightUnit) // Include WeightUnit
                .Include(c => c.LogisticsSaleOrderDetail)
                    .ThenInclude(l => l.ReasonForExport) // Include ReasonForExport
                .Include(c => c.LogisticsSaleOrderDetail)
                    .ThenInclude(l => l.Currency) // Include Currency
                //.Include(c => c.LogisticsSaleOrderDetail)
                //    .ThenInclude(l => l.VehicleType) // Include VehicleType
                .Include(c => c.LogisticsSaleOrderDetail)
                    .ThenInclude(l => l.LogisticsSaleOrderProductsItems)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<SalesOrderDto>.Return404();
            }
            var dto = _mapper.Map<SalesOrderDto>(entity);
            return ServiceResponse<SalesOrderDto>.ReturnResultWith200(dto);
        }
    }
}
