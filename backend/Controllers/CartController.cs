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
            var carts = await _cartService.GetAllCarts();
            if (carts == null)
            {
                return BadRequest("An error occured while retrieving Carts.");
            }
            return Ok(carts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _cartService.GetCart(id);
            if (cart == null)
            {
                return BadRequest("An error occured while retrieving Cart.");
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CreateCart createCart)
        {
            Cart cart = await _cartService.CreateCart(createCart);
            if (cart == null)
            {
                return BadRequest("Please provide a correct status for cart.");
            }
            return Ok(cart);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCart updateCart)
        {
            Cart cart = await _cartService.UpdateCart(id, updateCart);
            if (cart == null)
            {
                return BadRequest("Please provide a correct status for cart.");
            }
            return Ok(cart);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddPizzaToCart(int id, [FromBody] AddPizza addPizza)
        {
            bool addedPizzaToCart = await _cartService.AddPizzaToCart(id, addPizza);
            if (!addedPizzaToCart)
            {
                return BadRequest("An error occured while adding pizzas to cart.");
            }
            return Ok(addedPizzaToCart);
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetTodayCarts()
        {
            List<Cart> carts = await _cartService.GetTodayCarts();
            if (carts == null)
            {
                return BadRequest("An error occured while trying to get daily carts.");
            }
            return Ok(carts);
        }
    }
}
