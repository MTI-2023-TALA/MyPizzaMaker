namespace backend.DataAccess.Interfaces
{
    public interface ICartRepository: DataAccess.IRepository<EfModels.Cart, Dto.Cart>
    {
        public List<Dto.Cart> GetAllCarts();
        public Dto.Cart GetCart(long cartId);
        public List<Dto.Cart> GetTodayCarts();
        public int GetTodayStats();
        public int GetWeeklyStats();
        public int GetMonthlyStats();
    }
}
