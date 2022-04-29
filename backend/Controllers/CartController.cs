using Microsoft.AspNetCore.Mvc;
using backend.DataAccess.Interfaces;
using backend.Service.Interfaces;
using backend.Dto;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            return Ok(await _cartService.GetAllCarts());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCart(int id)
        {
            return Ok(await _cartService.GetCart(id));
        }

        [HttpPost]
        public async Task<Cart> CreateCart([FromBody] CreateCart createCart)
        {
            return await _cartService.CreateCart(createCart);
        }

        [HttpPatch("{id:int}")]
        public async Task<Cart> UpdateCart(int id, [FromBody] UpdateCart updateCart)
        {
            return await _cartService.UpdateCart(id, updateCart);
        }

        [HttpPost("{id:int}")]
        public async void AddPizzaToCart(int id)
        {
            // TODO: to be done with cart adding system
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetTodayCarts()
        {
            return Ok(await _cartService.GetTodayCarts());
        }
    }
}
