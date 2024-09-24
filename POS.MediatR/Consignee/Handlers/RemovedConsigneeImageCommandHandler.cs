using AutoMapper;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class RemovedConsigneeImageCommandHandler : IRequestHandler<RemovedConsigneeImageCommand, ServiceResponse<ConsigneeDto>>
    {
        private readonly IConsigneeRepository _consigneeRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<RemovedConsigneeImageCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;
        public RemovedConsigneeImageCommandHandler(IConsigneeRepository consigneeRepository,
            ILogger<RemovedConsigneeImageCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper
            )
        {
            _consigneeRepository = consigneeRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<ConsigneeDto>> Handle(RemovedConsigneeImageCommand request, CancellationToken cancellationToken)
        {
            var entity = _consigneeRepository.Find(request.Id);
            if (entity == null)
            {
                _logger.LogError("Consignee does not exist.");
                return ServiceResponse<ConsigneeDto>.Return404("Consignee does not found.");
            }
            var oldImageUrl = entity.Url;
            entity.Url = string.Empty;
            _consigneeRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Save Consignee");
                return ServiceResponse<ConsigneeDto>.Return500();
            }

            // delete consignee image
            string contentRootPath = _webHostEnvironment.WebRootPath;
            var imgPath = Path.Combine(contentRootPath, _pathHelper.ConsigneeImagePath, oldImageUrl);
            if (File.Exists(imgPath))
            {
                FileData.DeleteFile(imgPath);
            }
            return ServiceResponse<ConsigneeDto>.ReturnResultWith200(_mapper.Map<ConsigneeDto>(entity));

        }


    }
}
