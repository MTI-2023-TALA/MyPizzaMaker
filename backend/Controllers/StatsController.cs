using Microsoft.AspNetCore.Mvc;
using backend.Service.Interfaces;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService, ILogger<StatsController> logger)
        {
            _statsService = statsService;
            _logger = logger;
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyStats()
        {
            return Ok(await _statsService.GetDailyStats());
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyStats()
        {
            return Ok(await _statsService.GetWeeklyStats());

        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyStats()
        {
            return Ok(await _statsService.GetMonthlyStats());
        }
    }
}
