using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetNewConsigneeQueryHandler : IRequestHandler<GetNewConsigneeQuery, List<ConsigneeDto>>
    {
        private readonly IConsigneeRepository _consigneeRepository;
        private readonly PathHelper _pathHelper;
        public GetNewConsigneeQueryHandler(IConsigneeRepository consigneeRepository,
            PathHelper pathHelper
            )
        {
            _consigneeRepository = consigneeRepository;
            _pathHelper = pathHelper;
        }

        public async Task<List<ConsigneeDto>> Handle(GetNewConsigneeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _consigneeRepository.All.Where(cs => cs.IsVarified && !cs.IsDeleted)
                .OrderByDescending(c => c.CreatedDate).Take(10)
                .Select(c => new ConsigneeDto
                {
                    Id = c.Id,
                    ConsigneeName = c.ConsigneeName,
                    Url = string.IsNullOrWhiteSpace(c.Url) ? _pathHelper.NoImageFound : Path.Combine(_pathHelper.ConsigneeImagePath, c.Url),
                    Description = c.Description
                }).ToListAsync();
            return entities;
        }
    }
}
