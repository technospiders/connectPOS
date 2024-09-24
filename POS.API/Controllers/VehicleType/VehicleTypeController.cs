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
    /// VehicleType Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public VehicleTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all Reasons for Export
        /// </summary>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<VehicleTypeDto>))]
        public async Task<IActionResult> GetReasonsForExport([FromQuery] GetAllVehicleTypeQuery getAllVehicleTypeQuery)
        {
            var result = await _mediator.Send(getAllVehicleTypeQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Reason for Export by Id
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(VehicleTypeDto))]
        public async Task<IActionResult> GetVehicleType(Guid id)
        {
            var getVehicleTypeQuery = new GetVehicleTypeByIdQuery { Id = id };
            var result = await _mediator.Send(getVehicleTypeQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create a new Reason for Export
        /// </summary>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(VehicleTypeDto))]
        public async Task<IActionResult> AddVehicleType(AddVehicleTypeCommand addVehicleTypeCommand)
        {
            var response = await _mediator.Send(addVehicleTypeCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetVehicleType", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update a Reason for Export
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(VehicleTypeDto))]
        public async Task<IActionResult> UpdateVehicleType(Guid id, UpdateVehicleTypeCommand updateVehicleTypeCommand)
        {
            updateVehicleTypeCommand.Id = id;
            var result = await _mediator.Send(updateVehicleTypeCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete a Reason for Export
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(Guid id)
        {
            var deleteVehicleTypeCommand = new DeleteVehicleTypeCommand { Id = id };
            var result = await _mediator.Send(deleteVehicleTypeCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
