namespace backend.DataAccess.Interfaces
{
    public interface ICartRepository: DataAccess.IRepository<EfModels.Cart, Dbo.Cart>
    {
        public List<Dbo.Cart> GetAllCarts();
        public List<Dbo.Cart> GetTodayCarts();
        public int GetTodayStats();
        public int GetWeeklyStats();
        public int GetMonthlyStats();
    }
}
