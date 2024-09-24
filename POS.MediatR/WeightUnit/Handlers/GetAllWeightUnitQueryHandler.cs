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
    public class GetAllWeightUnitQueryHandler : IRequestHandler<GetAllWeightUnitQuery, List<WeightUnitDto>>
    {
        private readonly IWeightUnitRepository _weightUnitRepository;
        private readonly IMapper _mapper;

        public GetAllWeightUnitQueryHandler(IWeightUnitRepository weightUnitRepository, IMapper mapper)
        {
            _weightUnitRepository = weightUnitRepository;
            _mapper = mapper;
        }
        public async Task<List<WeightUnitDto>> Handle(GetAllWeightUnitQuery request, CancellationToken cancellationToken)
        {
            var categories = await _weightUnitRepository.All
                .Where(c => request.IsDropDown)
                .OrderBy(c => c.UnitName)
                .ProjectTo<WeightUnitDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return categories;
        }
    }
}
