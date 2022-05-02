using AutoMapper;
using backend.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Service
{
    public class StatsService : Interfaces.IStatsService
    {
        private readonly ILogger<StatsService> _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IIngredientRepository _ingredientRepository;
        protected readonly IMapper _mapper;

        public StatsService(ICartRepository cartRepository, IIngredientRepository ingredientRepository, ILogger<StatsService> logger, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _ingredientRepository = ingredientRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> GetDailyStats()
        {
            return _cartRepository.GetDailyStats();
        }

        public async Task<int> GetWeeklyStats()
        {
            return _cartRepository.GetWeeklyStats();
        }

        public async Task<int> GetMonthlyStats()
        {
            return _cartRepository.GetMonthlyStats();
        }

        public async Task<List<Dto.IngredientStats>> GetIngredientsStats()
        {
            List<Dbo.IngredientStats> stats = _ingredientRepository.GetIngredientsStats();
            return _mapper.Map<List<Dto.IngredientStats>>(stats);
        }
    }
}
