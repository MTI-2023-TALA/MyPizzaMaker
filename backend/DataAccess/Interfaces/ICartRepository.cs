namespace backend.DataAccess.Interfaces
{
    public interface ICartRepository: DataAccess.IRepository<EfModels.Cart, Dbo.Cart>
    {
        public List<Dbo.Cart> GetAllCarts();
        public List<Dbo.Cart> GetTodayCarts();
        public int GetDailyStats();
        public int GetWeeklyStats();
        public int GetMonthlyStats();
        public Task<bool> AddPizzaToCart(int pizzaId, int cartId);
        public List<Dbo.CartPizzaWithName> GetPizzasfromCart(int cartId);
        public bool ValidateCartStatus(string status);
    }
}
