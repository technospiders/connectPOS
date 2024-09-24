using POS.Data.Resources;
using POS.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using POS.API.Helpers;
using POS.MediatR.Supplier.Commands;

namespace POS.API.Controllers.Consignee
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsigneeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConsigneeController> _logger;

        public ConsigneeController(IMediator mediator, ILogger<ConsigneeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "GetConsignees")]
        [ClaimCheck("CUST_VIEW_CUSTOMERS")]
        public async Task<IActionResult> GetConsignees([FromQuery] ConsigneeResource consigneeResource)
        {
            var query = new GetAllConsigneeQuery { ConsigneeResource = consigneeResource };
            var result = await _mediator.Send(query);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }

        ////[HttpGet(Name = "GetConsigneeForCustomer")]
        ////[ClaimCheck("CUST_VIEW_CUSTOMERS")]
        ////public async Task<IActionResult> GetConsigneeForCustomer([FromQuery] ConsigneeResource consigneeResource)
        ////{
        ////    if (consigneeResource == null || consigneeResource.CustomerId == Guid.Empty)
        ////    {
        ////        _logger.LogError("CustomerId Should not Empty");
        ////        return null;
        ////    }
        ////    var query = new GetAllConsigneeQuery { ConsigneeResource = consigneeResource };
        ////    var result = await _mediator.Send(query);

        ////    var paginationMetadata = new
        ////    {
        ////        totalCount = result.TotalCount,
        ////        pageSize = result.PageSize,
        ////        skip = result.Skip,
        ////        totalPages = result.TotalPages
        ////    };
        ////    Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
        ////    return Ok(result);
        ////}

        [HttpGet("{id}", Name = "GetConsignee")]
        [ClaimCheck("CUST_VIEW_CUSTOMERS")]
        public async Task<IActionResult> GetConsignee(Guid id)
        {
            var query = new GetConsigneeQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode, JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return ReturnFormattedResponse(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        [ClaimCheck("CUST_ADD_CUSTOMER")]
        public async Task<IActionResult> AddConsignee([FromBody] AddConsigneeCommand addConsigneeCommand)
        {
            var result = await _mediator.Send(addConsigneeCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode, JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return CreatedAtRoute("GetConsignee", new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}"), DisableRequestSizeLimit]
        [ClaimCheck("CUST_UPDATE_CUSTOMER")]
        public async Task<IActionResult> UpdateConsignee(Guid id, [FromBody] UpdateConsigneeCommand updateConsigneeCommand)
        {
            updateConsigneeCommand.Id = id;
            var result = await _mediator.Send(updateConsigneeCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode, JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return ReturnFormattedResponse(result);
        }

        [HttpDelete("{id}")]
        [ClaimCheck("CUST_DELETE_CUSTOMER")]
        public async Task<IActionResult> DeleteConsignee(Guid id)
        {
            var command = new DeleteConsigneeCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode, JsonSerializer.Serialize(result.Errors), "");
                return StatusCode(result.StatusCode, result.Errors);
            }
            return ReturnFormattedResponse(result);
        }
    }


}
