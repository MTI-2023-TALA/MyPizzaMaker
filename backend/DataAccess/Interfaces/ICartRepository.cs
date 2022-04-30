namespace backend.DataAccess.Interfaces
{
    public interface ICartRepository: DataAccess.IRepository<EfModels.Cart, Dbo.Cart>
    {
        public List<Dbo.Cart> GetAllCarts();
        public List<Dbo.Cart> GetTodayCarts();
        public int GetDailyStats();
        public int GetWeeklyStats();
        public int GetMonthlyStats();

        public Task<bool> addPizzaToCart(int pizzaId, int cartId);
        public List<Dbo.CartPizzaWithName> getPizzasfromCart(int cartId);
    }
}
