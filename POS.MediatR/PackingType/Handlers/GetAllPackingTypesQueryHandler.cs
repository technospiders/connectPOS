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
    public class GetAllPackingTypesQueryHandler : IRequestHandler<GetAllPackingTypesQuery, List<PackingTypeDto>>
    {
        private readonly IPackingTypeRepository _packingTypeRepository;
        private readonly IMapper _mapper;

        public GetAllPackingTypesQueryHandler(IPackingTypeRepository packingTypeRepository, IMapper mapper)
        {
            _packingTypeRepository = packingTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<PackingTypeDto>> Handle(GetAllPackingTypesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _packingTypeRepository.All
                .Where(c => request.IsDropDown)
                .OrderBy(c => c.Name)
                .ProjectTo<PackingTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return categories;
        }
    }
}
