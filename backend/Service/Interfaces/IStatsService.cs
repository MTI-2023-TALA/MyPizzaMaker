namespace backend.Service.Interfaces
{
    public interface IStatsService
    {
        public Task<int> GetDailyStats();
        public Task<int> GetWeeklyStats();
        public Task<int> GetMonthlyStats();
    }
}
