using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Data.Dto;
using POS.MediatR.Category.Commands;
using POS.MediatR.CommandAndQuery;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace POS.API.Controllers
{
    /// <summary>
    /// WeightUnit Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeightUnitController : BaseController
    {
        private readonly IMediator _mediator;

        public WeightUnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all Reasons for Export
        /// </summary>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<WeightUnitDto>))]
        public async Task<IActionResult> GetReasonsForExport([FromQuery] GetAllWeightUnitQuery getAllWeightUnitQuery)
        {
            var result = await _mediator.Send(getAllWeightUnitQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Reason for Export by Id
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(WeightUnitDto))]
        public async Task<IActionResult> GetWeightUnit(Guid id)
        {
            var getWeightUnitQuery = new GetWeightUnitByIdQuery { Id = id };
            var result = await _mediator.Send(getWeightUnitQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create a new Reason for Export
        /// </summary>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(WeightUnitDto))]
        public async Task<IActionResult> AddWeightUnit(AddWeightUnitCommand addWeightUnitCommand)
        {
            var response = await _mediator.Send(addWeightUnitCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetWeightUnit", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update a Reason for Export
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(WeightUnitDto))]
        public async Task<IActionResult> UpdateWeightUnit(Guid id, UpdateWeightUnitCommand updateWeightUnitCommand)
        {
            updateWeightUnitCommand.Id = id;
            var result = await _mediator.Send(updateWeightUnitCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete a Reason for Export
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeightUnit(Guid id)
        {
            var deleteWeightUnitCommand = new DeleteWeightUnitCommand { Id = id };
            var result = await _mediator.Send(deleteWeightUnitCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
