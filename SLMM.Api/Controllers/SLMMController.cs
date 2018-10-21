using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SLMM.Api.Extensions;
using SLMM.Api.Requests;
using SLMM.Domain;

namespace SLMM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SLMMController : ControllerBase
    {
        private readonly ILawnMowingMachine _machine;

        public SLMMController(ILawnMowingMachine machine)
        {
            _machine = machine;
        }

        [HttpGet("position")]
        [ProducesResponseType(typeof(TurnResponse), 200)]
        public Task<IActionResult> Position([FromQuery] PositionRequest request)
        {
            var position = _machine.Position;

            return Task.FromResult<IActionResult>(Ok(position.To<PositionResponse>()));
        }

        [HttpPost("advance")]
        [ProducesResponseType(typeof(AdvanceResponse), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<ActionResult<AdvanceResponse>> Advance([FromBody] AdvanceRequest request)
        {
            await _machine.AdvanceAsync(request.Steps);

            return _machine.Position.To<AdvanceResponse>();
        }

        [HttpPost("turn")]
        [ProducesResponseType(typeof(TurnResponse), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<ActionResult<TurnResponse>> Turn([FromBody] TurnRequest request)
        {
            await _machine.TurnAsync(request.Degrees);

            return _machine.Position.To<TurnResponse>();
        }
    }
}