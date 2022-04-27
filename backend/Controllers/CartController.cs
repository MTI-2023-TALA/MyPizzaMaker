using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void GetAllCarts()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpGet("{id:long}")]
        public void GetCart(long id)
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpPost]
        public void CreateCart()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpPatch("{id:long}")]
        public void UpdateCart(long id)
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpPost("{id:long}")]
        public void AddPizzaToCart(long id)
        {

        }

        [HttpGet("today")]
        public void GetTodayCarts()
        {

        }
    }
}
