using Microsoft.AspNetCore.Mvc;
using WaterJugChallenge.Entities;
using WaterJugChallenge.Services.Interface;

namespace WaterJugChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WaterJugController(IWaterJugSolver solver) : ControllerBase
    {
        private readonly IWaterJugSolver _solver = solver;

        [HttpPost("GetSolution")]
        public IActionResult Solve([FromBody] WaterJugRequest request)
        {
            if (request.XCapacity <= 0 || request.YCapacity <= 0 || request.ZAmountWanted <= 0)
            {
                return BadRequest(new { message = "All capacities must be positive integers" });
            }

            var solution = _solver.GetSolution(request.XCapacity, request.YCapacity, request.ZAmountWanted);

            return Ok(new { solution });
        }
    }
}
