using AutoMapper;
using backend.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Service
{
    public class CartService : Interfaces.ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly ICartRepository _cartRepository;
        protected readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, ILogger<CartService> logger, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Dto.Cart>> GetAllCarts()
        {
            List<Dbo.Cart> carts = _cartRepository.GetAllCarts();
            return _mapper.Map<List<Dto.Cart>>(carts);
        }

        public async Task<Dto.Cart> GetCart(int id)
        {
            Dbo.Cart cart = await _cartRepository.GetOne(id);
            return _mapper.Map<Dto.Cart>(cart);

        }

        public async Task<Dto.Cart> CreateCart(Dto.CreateCart createCart)
        {
            Dbo.Cart cart = new Dbo.Cart();
            cart.Status = createCart.Status;
            cart.Date = createCart.Date;

            var result = await _cartRepository.Insert(cart);
            return _mapper.Map<Dto.Cart>(result);
        }

        public async Task<Dto.Cart> UpdateCart(int id, Dto.UpdateCart updateCart)
        {
            Dbo.Cart cart = new Dbo.Cart();
            cart.Id = id;
            cart.Status = updateCart.Status;

            var result = await _cartRepository.Update(cart);
            return _mapper.Map<Dto.Cart>(result);
        }

        public async Task<bool> DeleteCart(int id)
        {
            return await _cartRepository.Delete(id);
        }

        public async Task<List<Dto.Cart>> GetTodayCarts()
        {
            List<Dbo.Cart> carts = _cartRepository.GetTodayCarts();
            return _mapper.Map<List<Dto.Cart>>(carts);
        }
    }
}
