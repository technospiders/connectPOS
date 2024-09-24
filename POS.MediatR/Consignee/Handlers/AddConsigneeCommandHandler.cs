using AutoMapper;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class AddConsigneeCommandHandler : IRequestHandler<AddConsigneeCommand, ServiceResponse<ConsigneeDto>>
    {
        private readonly IConsigneeRepository _supplierRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<AddConsigneeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public AddConsigneeCommandHandler(IConsigneeRepository supplierRepository,
            ILogger<AddConsigneeCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IMapper mapper,
              IWebHostEnvironment webHostEnvironment,
              PathHelper pathHelper)
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<ConsigneeDto>> Handle(AddConsigneeCommand request, CancellationToken cancellationToken)
        {
            if (request.IsImageUpload && !string.IsNullOrEmpty(request.Logo))
            {
                var imageUrl = Guid.NewGuid().ToString() + ".png";
                request.Url = imageUrl;
            }

            if (request.CustomerId == Guid.Empty)
            {
                _logger.LogError("Customer should be selected for insertion of consignee");
                return ServiceResponse<ConsigneeDto>.Return422("Customer should be selected for insertion of consignee");
            }

            var entity = await _supplierRepository.FindBy(c => c.ConsigneeName == request.ConsigneeName).FirstOrDefaultAsync();
            if (entity != null)
            {
                _logger.LogError("Consignee Name is already exist.");
                return ServiceResponse<ConsigneeDto>.Return422("Consignee Name is already exist.");
            }   
            
            // Ensure that CustomerId is set and valid
            entity = _mapper.Map<Data.Consignee>(request);
            entity.CustomerId = request.CustomerId;  // Set the customer ID


            entity = _mapper.Map<Data.Consignee>(request);
            _supplierRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Save Consignee");
                return ServiceResponse<ConsigneeDto>.Return500();
            }

            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(entity.Url))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.ConsigneeImagePath, entity.Url);
                await FileData.SaveFile(pathToSave, request.Logo);
            }
            return ServiceResponse<ConsigneeDto>.ReturnResultWith200(_mapper.Map<ConsigneeDto>(entity));
        }
    }
}
