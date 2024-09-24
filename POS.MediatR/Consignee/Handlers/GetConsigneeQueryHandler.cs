using AutoMapper;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetConsigneeQueryHandler : IRequestHandler<GetConsigneeQuery, ServiceResponse<ConsigneeDto>>
    {
        private readonly IConsigneeRepository _consigneeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetConsigneeQueryHandler> _logger;
        private readonly PathHelper _pathHelper;
        public GetConsigneeQueryHandler(
           IConsigneeRepository consigneeRepository,
            IMapper mapper,
            ILogger<GetConsigneeQueryHandler> logger,
             PathHelper pathHelper
            )
        {

            _mapper = mapper;
            _consigneeRepository = consigneeRepository;
            _logger = logger;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<ConsigneeDto>> Handle(GetConsigneeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _consigneeRepository.AllIncluding(c => c.ConsigneeAddress, b => b.BillingAddress, s => s.ShippingAddress)
                .Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (entity != null)
            {
                var entityDto = _mapper.Map<ConsigneeDto>(entity);
                entityDto.ImageUrl = string.IsNullOrWhiteSpace(entityDto.Url) ? ""
                    : Path.Combine(_pathHelper.ConsigneeImagePath, entityDto.Url);
                return ServiceResponse<ConsigneeDto>.ReturnResultWith200(entityDto);
            }
            else
            {
                _logger.LogError("User not found");
                return ServiceResponse<ConsigneeDto>.ReturnFailed(404, "User not found");
            }
        }


    }
}
