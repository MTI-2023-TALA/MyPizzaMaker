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
    }
}
