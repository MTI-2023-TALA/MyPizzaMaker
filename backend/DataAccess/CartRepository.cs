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

        public int GetTodayStats()
        {
            var result = GetTodayCarts();
            return result.Count;
        }

        public int GetWeeklyStats()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => DatesAreInTheSameWeek(c.Date, today));
            List<Dbo.Cart> res = _mapper.Map<List<Dbo.Cart>>(result);
            return res.Count;
        }

        public int GetMonthlyStats()
        {
            DateTime today = DateTime.Today;
            var result = _context.Carts.Where(c => c.Date.Month == today.Month);
            List<Dbo.Cart> res = _mapper.Map<List<Dbo.Cart>>(result);
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
