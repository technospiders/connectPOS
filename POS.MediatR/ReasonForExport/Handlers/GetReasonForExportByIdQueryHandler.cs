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
    public class GetReasonForExportByIdQueryHandler
        : IRequestHandler<GetReasonForExportByIdQuery, ServiceResponse<ReasonForExportDto>>
    {
        private readonly IReasonForExportRepository _reasonForExportRepository;
        private readonly IMapper _mapper;

        public GetReasonForExportByIdQueryHandler(
            IReasonForExportRepository reasonForExportRepository,
            IMapper mapper)
        {
            _reasonForExportRepository = reasonForExportRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ReasonForExportDto>> Handle(GetReasonForExportByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _reasonForExportRepository.FindAsync(request.Id);
            if (category == null)
            {
                return ServiceResponse<ReasonForExportDto>.Return404();
            }

            return ServiceResponse<ReasonForExportDto>.ReturnResultWith200(_mapper.Map<ReasonForExportDto>(category));
        }
    }
}
