using AutoMapper;
using POS.Common.UnitOfWork;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class UpdateConsigneeCommandHandler : IRequestHandler<UpdateConsigneeCommand, ServiceResponse<ConsigneeDto>>
    {
        private readonly IConsigneeRepository _consigneeRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<UpdateConsigneeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public UpdateConsigneeCommandHandler(IConsigneeRepository consigneeRepository,
            ILogger<UpdateConsigneeCommandHandler> logger,
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

        public async Task<ServiceResponse<ConsigneeDto>> Handle(UpdateConsigneeCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _consigneeRepository.FindBy(c => c.Id != request.Id && c.ConsigneeName == request.ConsigneeName.Trim())
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Consignee Name Already Exist for another consignee.");
                return ServiceResponse<ConsigneeDto>.Return422("Consignee Name Already Exist for another consignee.");
            }

            var entity = await _consigneeRepository
              .AllIncluding(c => c.ConsigneeAddress, cs => cs.ShippingAddress, b => b.BillingAddress)
              .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (request.IsImageUpload)
            {
                if (!string.IsNullOrEmpty(request.Logo))
                {
                    request.Url = Guid.NewGuid().ToString() + ".png";
                }
                else
                {
                    request.Url = "";
                }
            }
            else
            {
                request.Url = entity.Url;
            }

            var oldImageUrl = entity.Url;

            entity.CustomerId = request.CustomerId;  // Ensure the relationship

            entity = _mapper.Map(request, entity);
            _consigneeRepository.Update(entity);
            if (_uow.Save() <= 0)
            {
                _logger.LogError("Error to Update Consignee");
                return ServiceResponse<ConsigneeDto>.Return500();
            }

            if (request.IsImageUpload)
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                // delete old file
                if (!string.IsNullOrWhiteSpace(oldImageUrl)
                    && File.Exists(Path.Combine(contentRootPath, _pathHelper.ConsigneeImagePath, oldImageUrl)))
                {
                    FileData.DeleteFile(Path.Combine(contentRootPath, _pathHelper.ConsigneeImagePath, oldImageUrl));
                }
                // save new file
                if (!string.IsNullOrWhiteSpace(request.Logo))
                {
                    var parentPath = Path.Combine(contentRootPath, _pathHelper.ConsigneeImagePath);
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    await FileData.SaveFile(Path.Combine(parentPath, entity.Url), request.Logo);
                }
            }
            return ServiceResponse<ConsigneeDto>.ReturnResultWith200(_mapper.Map<ConsigneeDto>(entity));
        }
    }
}
