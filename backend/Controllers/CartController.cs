using Microsoft.AspNetCore.Mvc;
using backend.DataAccess.Interfaces;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository, ILogger<CartController> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCarts()
        {
            return Ok(_cartRepository.GetAllCarts());
        }

        [HttpGet("{id:long}")]
        public IActionResult GetCart(long id)
        {
            return Ok(_cartRepository.GetCart(id));
        }

        [HttpPost]
        public void CreateCart()
        {
            
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
        public IActionResult GetTodayCarts()
        {
            return Ok(_cartRepository.GetTodayCarts());
        }
    }
}
