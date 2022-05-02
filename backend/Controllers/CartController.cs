using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateCart([FromBody] CreateCart createCart)
        {
            Cart cart = await _cartService.CreateCart(createCart);
            if (cart == null)
            {
                return BadRequest("Please provide a correct status for cart!");
            }
            return Ok(cart);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCart updateCart)
        {
            Cart cart = await _cartService.UpdateCart(id, updateCart);
            if (cart == null)
            {
                return BadRequest("Please provide a correct status for cart!");
            }
            return Ok(cart);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddPizzaToCart(int id, [FromBody] AddPizza addPizza)
        {
            return Ok(await _cartService.AddPizzaToCart(id, addPizza));
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetTodayCarts()
        {
            return Ok(await _cartService.GetTodayCarts());
        }
    }
}
