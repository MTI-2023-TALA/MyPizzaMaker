using AutoMapper;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;

namespace backend.DataAccess
{
    public class CartRepository : Repository<EfModels.Cart, Dto.Cart>, ICartRepository
    {
        public CartRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Dto.Cart> GetAllCarts()
        {
            var result = _context.Carts.ToList();
            return _mapper.Map<List<Dto.Cart>>(result);
        }

        public Dto.Cart GetCart(long cartId)
        {
            var result = _context.Carts.Where(c => c.Id == cartId);
            return _mapper.Map<Dto.Cart>(result);
        }

        public List<Dto.Cart> GetTodayCarts()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => c.Date.Date == today.Date);
            return _mapper.Map<List<Dto.Cart>>(result);
        }

        public int GetTodayStats()
        {
            var result = GetTodayCarts();
            return result.Count;
        }

        public int GetWeeklyStats()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => DatesAreInTheSameWeek(c.Date, today));
            List<Dto.Cart> res = _mapper.Map<List<Dto.Cart>>(result);
            return res.Count;
        }

        public int GetMonthlyStats()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => c.Date.Month == today.Month);
            List<Dto.Cart> res = _mapper.Map<List<Dto.Cart>>(result);
            return res.Count;
        }

        private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));
            return d1 == d2;
        }
    }
}
