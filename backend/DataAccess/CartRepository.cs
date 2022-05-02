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
            var result = _context.Carts.ToList();
            return _mapper.Map<List<Dbo.Cart>>(result);
        }

        public List<Dbo.Cart> GetTodayCarts()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => c.Date.Date == today.Date);
            return _mapper.Map<List<Dbo.Cart>>(result);
        }

        public int GetDailyStats()
        {
            var result = GetTodayCarts();
            return result.Count;
        }

        public int GetWeeklyStats()
        {
            var result = _context.Carts.Where(c => c.Date.DayOfYear - DateTime.Today.DayOfYear <= 7);
            return result.Count();
        }

        public int GetMonthlyStats()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => c.Date.Month == today.Month && c.Date.Year == today.Year);
            return result.Count();
        }

        public async Task<bool> AddPizzaToCart(int pizzaId, int cartId)
        {
            try
            {
                Dbo.CartsPizza cartsPizza = new Dbo.CartsPizza();
                cartsPizza.PizzaId = pizzaId;
                cartsPizza.CartId = cartId;

                EfModels.CartsPizza cartsPizzaModel = _mapper.Map<EfModels.CartsPizza>(cartsPizza);
                _context.CartsPizzas.Add(cartsPizzaModel);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception e)
            {
                _logger.LogError("Unable to insert data to the database in CartsPizza", e);
                return false;
            }
        }

        public List<Dbo.CartPizzaWithName> GetPizzasfromCart(int cartId)
        {
            try
            {
                List<Dbo.CartPizzaWithName> result = _context.CartsPizzas.Where(cp => cp.CartId == cartId).Join(
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
    }
}
