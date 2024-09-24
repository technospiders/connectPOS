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
    /// PackingType Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PackingTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public PackingTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all Packing Types
        /// </summary>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<PackingTypeDto>))]
        public async Task<IActionResult> GetPackingTypes([FromQuery] GetAllPackingTypesQuery getAllPackingTypesQuery)
        {
            var result = await _mediator.Send(getAllPackingTypesQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Packing Type by Id
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(PackingTypeDto))]
        public async Task<IActionResult> GetPackingType(Guid id)
        {
            var getPackingTypeQuery = new GetPackingTypeByIdQuery { Id = id };
            var result = await _mediator.Send(getPackingTypeQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create a new Packing Type
        /// </summary>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(PackingTypeDto))]
        public async Task<IActionResult> AddPackingType(AddPackingTypeCommand addPackingTypeCommand)
        {
            var response = await _mediator.Send(addPackingTypeCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetPackingType", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update a Packing Type
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(PackingTypeDto))]
        public async Task<IActionResult> UpdatePackingType(Guid id, UpdatePackingTypeCommand updatePackingTypeCommand)
        {
            updatePackingTypeCommand.Id = id;
            var result = await _mediator.Send(updatePackingTypeCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete a Packing Type
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackingType(Guid id)
        {
            var deletePackingTypeCommand = new DeletePackingTypeCommand { Id = id };
            var result = await _mediator.Send(deletePackingTypeCommand);
            return ReturnFormattedResponse(result);
        }
    }
}