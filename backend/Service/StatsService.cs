using AutoMapper;
using backend.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Service
{
    public class StatsService : Interfaces.IStatsService
    {
        private readonly ILogger<StatsService> _logger;
        private readonly ICartRepository _cartRepository;
        protected readonly IMapper _mapper;

        public StatsService(ICartRepository cartRepository, ILogger<StatsService> logger, IMapper mapper)
        {
            _cartRepository = cartRepository;
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
    }
}
