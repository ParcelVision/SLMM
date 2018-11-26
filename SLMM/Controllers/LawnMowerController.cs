using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SLMM.Common.Navigation;
using SLMM.Core.Driving;
using SLMM.Models;

namespace SLMM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawnMowerController : ControllerBase
    {
        private readonly ILawnMowerDriver _driver;

        public LawnMowerController(ILawnMowerDriver driver)
        {
            _driver = driver;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var position = await _driver.GetPositionAsync();
            var orientation = await _driver.GetOrientation();
            return Ok(new StatusResponseModel
            {
                Position = position,
                Orientation = orientation
            });
        }

        [HttpPost("advance")]
        public async Task<IActionResult> AdvanceAsync()
        {
            if (await _driver.AdvanceAsync()) return Ok();
            return BadRequest();
        }

        [HttpPost("turn")]
        public async Task<IActionResult> TurnAsync([FromQuery] TurnDirection direction = TurnDirection.Clockwise)
        {
            if (await _driver.TurnAsync(direction)) return Ok();
            return BadRequest();
        }
    }
}