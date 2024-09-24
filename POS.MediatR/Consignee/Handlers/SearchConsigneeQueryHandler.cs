using POS.Data.Dto;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class SearchConsigneeQueryHandler : IRequestHandler<SearchConsigneeQuery, List<ConsigneeDto>>
    {
        private readonly IConsigneeRepository _consigneeRepository;


        public SearchConsigneeQueryHandler(IConsigneeRepository consigneeRepository)
        {
            _consigneeRepository = consigneeRepository;
        }
        public async Task<List<ConsigneeDto>> Handle(SearchConsigneeQuery request, CancellationToken cancellationToken)
        {
            var consignees = await _consigneeRepository.All.Where(a => EF.Functions.Like(a.ConsigneeName, $"{request.SearchQuery}%"))
                .Take(request.PageSize)
                .Select(c => new ConsigneeDto
                {
                    Id = c.Id,
                    ConsigneeName = c.ConsigneeName
                }).ToListAsync();
            return consignees;
        }
    }
}
