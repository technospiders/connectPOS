using AutoMapper;
using MediatR;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Category.Commands;
using POS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Category.Handlers
{
    public class GetPackingTypeByIdQueryHandler
        : IRequestHandler<GetPackingTypeByIdQuery, ServiceResponse<PackingTypeDto>>
    {
        private readonly IPackingTypeRepository _packingTypeRepository;
        private readonly IMapper _mapper;

        public GetPackingTypeByIdQueryHandler(
            IPackingTypeRepository packingTypeRepository,
            IMapper mapper)
        {
            _packingTypeRepository = packingTypeRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<PackingTypeDto>> Handle(GetPackingTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _packingTypeRepository.FindAsync(request.Id);
            if (category == null)
            {
                return ServiceResponse<PackingTypeDto>.Return404();
            }

            return ServiceResponse<PackingTypeDto>.ReturnResultWith200(_mapper.Map<PackingTypeDto>(category));
        }
    }
}
