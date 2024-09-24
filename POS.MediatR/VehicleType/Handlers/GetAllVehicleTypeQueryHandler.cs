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
    public class GetAllVehicleTypeQueryHandler : IRequestHandler<GetAllVehicleTypeQuery, List<VehicleTypeDto>>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IMapper _mapper;

        public GetAllVehicleTypeQueryHandler(IVehicleTypeRepository vehicleTypeRepository, IMapper mapper)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<VehicleTypeDto>> Handle(GetAllVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            var categories = await _vehicleTypeRepository.All
                .Where(c => request.IsDropDown)
                .OrderBy(c => c.VehicleTypeName)
                .ProjectTo<VehicleTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return categories;
        }
    }
}
