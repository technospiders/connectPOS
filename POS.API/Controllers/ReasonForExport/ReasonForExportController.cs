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
    /// ReasonForExport Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReasonForExportController : BaseController
    {
        private readonly IMediator _mediator;

        public ReasonForExportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all Reasons for Export
        /// </summary>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<ReasonForExportDto>))]
        public async Task<IActionResult> GetReasonsForExport([FromQuery] GetAllReasonForExportsQuery getAllReasonForExportQuery)
        {
            var result = await _mediator.Send(getAllReasonForExportQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Reason for Export by Id
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(ReasonForExportDto))]
        public async Task<IActionResult> GetReasonForExport(Guid id)
        {
            var getReasonForExportQuery = new GetReasonForExportByIdQuery { Id = id };
            var result = await _mediator.Send(getReasonForExportQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create a new Reason for Export
        /// </summary>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(ReasonForExportDto))]
        public async Task<IActionResult> AddReasonForExport(AddReasonForExportCommand addReasonForExportCommand)
        {
            var response = await _mediator.Send(addReasonForExportCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetReasonForExport", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update a Reason for Export
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(ReasonForExportDto))]
        public async Task<IActionResult> UpdateReasonForExport(Guid id, UpdateReasonForExportCommand updateReasonForExportCommand)
        {
            updateReasonForExportCommand.Id = id;
            var result = await _mediator.Send(updateReasonForExportCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete a Reason for Export
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReasonForExport(Guid id)
        {
            var deleteReasonForExportCommand = new DeleteReasonForExportCommand { Id = id };
            var result = await _mediator.Send(deleteReasonForExportCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
