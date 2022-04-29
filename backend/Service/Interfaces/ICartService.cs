namespace backend.Service.Interfaces
{
    public interface ICartService
    {
        public Task<List<Dto.Cart>> GetAllCarts();
        public Task<Dto.Cart> GetCart(int id);
        public Task<Dto.Cart> CreateCart(Dto.CreateCart createCart);
        public Task<Dto.Cart> UpdateCart(int id, Dto.UpdateCart updateCart);
        public Task<bool> DeleteCart(int id);
        public Task<List<Dto.Cart>> GetTodayCarts();
    }
}
