using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;

        public StatsController(ILogger<StatsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("today")]
        public void GetTodayStats()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpGet("weekly")]
        public void GetWeeklyStats()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpGet("monthly")]
        public void GetMonthly()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }
    }
}
