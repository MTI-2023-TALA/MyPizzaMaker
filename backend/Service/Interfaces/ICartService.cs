namespace backend.Service.Interfaces
{
    public interface ICartService
    {
        public Task<List<Dto.CartPizzaIngredient>> GetAllCarts();
        public Task<Dto.CartPizzaIngredient> GetCart(int id);
        public Task<Dto.Cart> CreateCart(Dto.CreateCart createCart);
        public Task<Dto.Cart> UpdateCart(int id, Dto.UpdateCart updateCart);
        public Task<List<Dto.Cart>> GetTodayCarts();
        public Task<bool> AddPizzaToCart(int cartId, Dto.AddPizza addPizza);
    }
}
