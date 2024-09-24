using AutoMapper;
using MediatR;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Category.Commands;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Category.Handlers
{
    public class GetWeightUnitByIdQueryHandler
        : IRequestHandler<GetWeightUnitByIdQuery, ServiceResponse<WeightUnitDto>>
    {
        private readonly IWeightUnitRepository _weightUnitRepository;
        private readonly IMapper _mapper;

        public GetWeightUnitByIdQueryHandler(
            IWeightUnitRepository weightUnitRepository,
            IMapper mapper)
        {
            _weightUnitRepository = weightUnitRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<WeightUnitDto>> Handle(GetWeightUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _weightUnitRepository.FindAsync(request.Id);
            if (category == null)
            {
                return ServiceResponse<WeightUnitDto>.Return404();
            }

            return ServiceResponse<WeightUnitDto>.ReturnResultWith200(_mapper.Map<WeightUnitDto>(category));
        }
    }
}
