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
            var dailyStats = await _statsService.GetDailyStats();
            if (dailyStats == -1)
            {
                return BadRequest("An error occured while retrieving daily stats.");
            }
            return Ok(dailyStats);
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyStats()
        {
            var weeklyStats = await _statsService.GetWeeklyStats();
            if (weeklyStats == -1)
            {
                return BadRequest("An error occured while retrieving weekly stats.");
            }
            return Ok(weeklyStats);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyStats()
        {
            var  monthlyStats = await _statsService.GetMonthlyStats();
            if (monthlyStats == -1)
            {
                return BadRequest("An error occured while retrieving monthly stats.");
            }
            return Ok(monthlyStats);
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> GetIngredientsStats()
        {
            var ingredientsStats = await _statsService.GetIngredientsStats();
            if (ingredientsStats == null)
            {
                return BadRequest("An error occured while retrieving ingredients stats.");
            }
            return Ok(ingredientsStats);
        }
    }
}
