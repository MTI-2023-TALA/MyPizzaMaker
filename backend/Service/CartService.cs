using AutoMapper;
using backend.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Service
{
    public class CartService : Interfaces.ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IPizzaRepository _pizzaRepository;
        protected readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IPizzaRepository pizzaRepository, ILogger<CartService> logger, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _pizzaRepository = pizzaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Dto.CartPizzaIngredient>> GetAllCarts()
        {
            List<Dto.CartPizzaIngredient> cartPizzas = new List<Dto.CartPizzaIngredient>();
            
            // getting all carts
            List<Dbo.Cart> carts = _cartRepository.GetAllCarts();

            foreach (var cart in carts)
            {
                // getting all pizzas and ingredients from cart
                Dto.CartPizzaIngredient cartPizzaIngredient = this.GetCartPizzasIngredients(cart);
                cartPizzas.Add(cartPizzaIngredient);
            }

            return cartPizzas;
        }

        public async Task<Dto.CartPizzaIngredient> GetCart(int id)
        {
            Dbo.Cart cart = await _cartRepository.GetOne(id);
            return GetCartPizzasIngredients(cart);
        }

        private Dto.CartPizzaIngredient GetCartPizzasIngredients(Dbo.Cart cart)
        {
            // get pizzas associated to cart
            List<Dbo.CartPizzaWithName> pizzas = _cartRepository.GetPizzasfromCart(cart.Id);
            List<Dto.CartPizzaIngredient.Pizza> pizzasMapped = new List<Dto.CartPizzaIngredient.Pizza>();
            foreach (var pizza in pizzas)
            {
                // get ingredients associated to pizza
                List<Dbo.Ingredient> ingredients = _pizzaRepository.GetPizzaIngredients(pizza.PizzaId);
                List<Dto.CartPizzaIngredient.Ingredient> ingredientsMapped = _mapper.Map<List<Dto.CartPizzaIngredient.Ingredient>>(ingredients);

                Dto.CartPizzaIngredient.Pizza pizzaToReturn = new Dto.CartPizzaIngredient.Pizza();
                pizzaToReturn.PizzaId = pizza.PizzaId;
                pizzaToReturn.Name = pizza.PizzaName;
                pizzaToReturn.Ingredients = ingredientsMapped;
                pizzasMapped.Add(pizzaToReturn);
            }

            Dto.CartPizzaIngredient cartPizzaIngredient = new Dto.CartPizzaIngredient();
            cartPizzaIngredient.CartId = cart.Id;
            cartPizzaIngredient.Status = cart.Status;
            cartPizzaIngredient.Date = cart.Date;
            cartPizzaIngredient.Pizzas = pizzasMapped;
            return cartPizzaIngredient;
        }

        public async Task<Dto.Cart> CreateCart(Dto.CreateCart createCart)
        {
            // verify cart status
            if (!_cartRepository.ValidateCartStatus(createCart.Status))
            {
                return null;
            }

            Dbo.Cart cart = new Dbo.Cart();
            cart.Status = createCart.Status;
            cart.Date = createCart.Date;

            var result = await _cartRepository.Insert(cart);
            return _mapper.Map<Dto.Cart>(result);
        }

        public async Task<Dto.Cart> UpdateCart(int id, Dto.UpdateCart updateCart)
        {
            if (!_cartRepository.ValidateCartStatus(updateCart.Status))
            {
                return null;
            }

            Dbo.Cart cart = new Dbo.Cart();
            cart.Id = id;
            cart.Status = updateCart.Status;

            var result = await _cartRepository.Update(cart);
            return _mapper.Map<Dto.Cart>(result);
        }

        public async Task<bool> DeleteCart(int id)
        {
            return await _cartRepository.Delete(id);
        }

        public async Task<List<Dto.Cart>> GetTodayCarts()
        {
            List<Dbo.Cart> carts = _cartRepository.GetTodayCarts();
            return _mapper.Map<List<Dto.Cart>>(carts);
        }

        public async Task<bool> AddPizzaToCart(int cartId, Dto.AddPizza addPizza)
        {
            // add pizza in db
            Dbo.Pizza pizza = new Dbo.Pizza();
            pizza.Name = addPizza.Name;
            Dbo.Pizza createdPizza = await _pizzaRepository.Insert(pizza);

            // add ingredients
            bool addedIngredients = await _pizzaRepository.AddPizzaIngredients(createdPizza.Id, addPizza.IngredientIds);

            // add pizza to cart
            bool addedPizzaToCart = await _cartRepository.AddPizzaToCart(createdPizza.Id, cartId);
            return true;
        }
    }
}
