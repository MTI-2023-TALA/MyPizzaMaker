using Microsoft.AspNetCore.Mvc;
using backend.DataAccess.Interfaces;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;
        private readonly ICartRepository _cartRepository;

        public StatsController(ICartRepository cartRepository, ILogger<StatsController> logger)
        {
            _logger = logger;
            _cartRepository = cartRepository;
        }

        [HttpGet("today")]
        public IActionResult GetTodayStats()
        {
            return Ok(_cartRepository.GetTodayStats());
        }

        [HttpGet("weekly")]
        public IActionResult GetWeeklyStats()
        {
            return Ok(_cartRepository.GetWeeklyStats());

        }

        [HttpGet("monthly")]
        public IActionResult GetMonthlyStats()
        {
            return Ok(_cartRepository.GetMonthlyStats());
        }
    }
}
