using AutoMapper;
using POS.Data.Dto;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace POS.MediatR.Handler
{
    public class GetAllReasonForExportsQueryHandler : IRequestHandler<GetAllReasonForExportsQuery, List<ReasonForExportDto>>
    {
        private readonly IReasonForExportRepository _reasonForExportRepository;
        private readonly IMapper _mapper;

        public GetAllReasonForExportsQueryHandler(IReasonForExportRepository reasonForExportRepository, IMapper mapper)
        {
            _reasonForExportRepository = reasonForExportRepository;
            _mapper = mapper;
        }
        public async Task<List<ReasonForExportDto>> Handle(GetAllReasonForExportsQuery request, CancellationToken cancellationToken)
        {
            var categories = await _reasonForExportRepository.All
                .Where(c => request.IsDropDown)
                .OrderBy(c => c.Name)
                .ProjectTo<ReasonForExportDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return categories;
        }
    }
}
