using AutoMapper;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;

namespace backend.DataAccess
{
    public class CartRepository : Repository<EfModels.Cart, Dbo.Cart>, ICartRepository
    {
        public CartRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Dbo.Cart> GetAllCarts()
        {
            try
            {
                var result = _context.Carts.ToList();
                return _mapper.Map<List<Dbo.Cart>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data in GetAllCarts", e);
                return null;
            }
        }

        public List<Dbo.Cart> GetTodayCarts()
        {
            try
            {
                DateTime today = DateTime.Today;
                var result = _context.Carts.Where(c => c.Date.Date == today.Date);
                return _mapper.Map<List<Dbo.Cart>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data in GetTodayCarts", e);
                return null;
            }
        }

        public int GetDailyStats()
        {
            try
            {
                var result = GetTodayCarts();
                return result.Count;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data in GetDailyStats", e);
                return -1;
            }
        }

        public int GetWeeklyStats()
        {
            try
            {
                var result = _context.Carts.Where(c => c.Date.DayOfYear - DateTime.Today.DayOfYear <= 7);
                return result.Count();
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data in GetWeeklyStats", e);
                return -1;
            }
        }

        public int GetMonthlyStats()
        {
            try
            {
                DateTime today = DateTime.Today;
                var result = _context.Carts.Where(c => c.Date.Month == today.Month && c.Date.Year == today.Year);
                return result.Count();
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data in GetMonthlyStats", e);
                return -1;
            }
        }

        public async Task<bool> AddPizzaToCart(int pizzaId, int cartId)
        {
            try
            {
                Dbo.CartsPizza cartsPizza = new Dbo.CartsPizza();
                cartsPizza.PizzaId = pizzaId;
                cartsPizza.CartId = cartId;

                CartsPizza cartsPizzaModel = _mapper.Map<CartsPizza>(cartsPizza);
                _context.CartsPizzas.Add(cartsPizzaModel);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception e)
            {
                _logger.LogError("Unable to insert data to the database in AddPizzaToCart", e);
                return false;
            }
        }

        public List<Dbo.CartPizzaWithName> GetPizzasfromCart(int cartId)
        {
            try
            {
                List<Dbo.CartPizzaWithName> result = _context.CartsPizzas.Where(cp => cp.CartId == cartId)
                    .Join(
                        _context.Pizzas,
                        cp => cp.PizzaId,
                        p => p.Id,
                        (cp, p) => new Dbo.CartPizzaWithName(p.Id, cp.CartId, p.Name)
                    ).ToList();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from the database in getPizzasFromCart", e);
                return null;
            }
        }

        public bool ValidateCartStatus(string status)
        {
            try
            {
                List<string> authorized_values = new List<String>()
                {
                    "in creation",
                    "waiting for confirmation",
                    "in preparation",
                    "to be collected",
                    "served",
                    "cancelled",
                };
                return authorized_values.Contains(status);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured in ValidateCartStatus.", e);
                return false;
            }
        }
    }
}
