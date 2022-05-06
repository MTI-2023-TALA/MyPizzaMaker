using backend.DataAccess;
using backend.DataAccess.EfModels;
using backend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendTest.services
{
    public class StatsServiceTest : IDisposable
    {
        private readonly ICartService _cartService;
        private readonly IIngredientService _ingredientService;
        private readonly IStatsService _statsService;

        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public StatsServiceTest()
        {
            var guid = Guid.NewGuid().ToString();

            // service & database
            var services = new ServiceCollection();
            services.AddDbContext<myPizzaMakerContext>(options =>
            {
                options.UseInMemoryDatabase(guid);
            });

            var options = new DbContextOptionsBuilder<myPizzaMakerContext>()
                .UseInMemoryDatabase(databaseName: guid)
                .Options;
            _options = options;

            // logger
            services.AddLogging();
            services.AddSingleton(typeof(ILogger), typeof(Logger<backend.Logger>));

            // automapper
            services.AddAutoMapper(typeof(AutomapperProfiles));
            services.AddTransient<backend.DataAccess.Interfaces.ICartRepository, CartRepository>();
            services.AddTransient<backend.DataAccess.Interfaces.IIngredientRepository, IngredientRepository>();
            services.AddTransient<backend.DataAccess.Interfaces.IPizzaRepository, PizzaRepository>();
            services.AddTransient<IIngredientService, backend.Service.IngredientService>();
            services.AddTransient<ICartService, backend.Service.CartService>();
            services.AddTransient<IStatsService, backend.Service.StatsService>();
            var serviceProvider = services.BuildServiceProvider();
            _cartService = serviceProvider.GetService<ICartService>();
            _ingredientService = serviceProvider.GetService<IIngredientService>();
            _statsService = serviceProvider.GetService<IStatsService>();
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async void TestGetDailyStats()
        {
            DateTime now = DateTime.Now;
            DateTime dateTime1 = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                now.Hour % 2 == 0 ? 5 : 17,
                now.Minute,
                0
            );
            DateTime dateTime2 = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                now.Hour % 2 == 0 ? 3 : 13,
                now.Minute,
                0
            );

            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = dateTime1,
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in preparation",
                Date = dateTime2,
            });

            int dailyStats = await _statsService.GetDailyStats();
            Assert.Equal(2, dailyStats);
        }

        [Fact]
        public async void TestGetWeeklyStats()
        {
            DateTime today = DateTime.Today;
            DateTime dateTime1 = today.AddDays(-3);
            DateTime dateTime2 = today.AddDays(-4);
            DateTime dateTime3 = today.AddDays(-8);

            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "waiting for confirmation",
                Date = dateTime1,
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in preparation",
                Date = dateTime2,
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in preparation",
                Date = dateTime3,
            });

            int weeklyStats = await _statsService.GetWeeklyStats();
            Assert.Equal(2, weeklyStats);
        }

        [Fact]
        public async void TestGetMonthlyStats()
        {
            DateTime now = DateTime.Now;
            DateTime dateTime1 = new DateTime(
                now.Year,
                now.Month,
                now.Day % 2 == 0 ? 5 : 24,
                now.Hour,
                now.Minute,
                0
            );
            DateTime dateTime2 = new DateTime(
                now.Year,
                now.Month,
                now.Day % 2 == 0 ? 3 : 16,
                now.Hour,
                now.Minute,
                0
            );

            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "to be collected",
                Date = dateTime1,
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "cancelled",
                Date = dateTime2,
            });

            int monthlyStats = await _statsService.GetMonthlyStats();
            Assert.Equal(2, monthlyStats);
        }

        [Fact]
        public async void TestGetIngredientsStats()
        {
            //------- TEST SETUP
            // create carts
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = new DateTime(2022, 12, 20),
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "served",
                Date = new DateTime(2022, 12, 22),
            });

            // create ingredients
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Dough",
                IsAvailable = true,
                Category = "dough",
            });
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Cheese",
                IsAvailable = true,
                Category = "cheese",
            });

            // create Dtos to send
            backend.Dto.AddPizza addPizzaDto = new backend.Dto.AddPizza
            {
                Name = "Pizza 1",
                IngredientIds = new List<int> { 1, 2 },
            };
            backend.Dto.AddPizza addPizza2Dto = new backend.Dto.AddPizza
            {
                Name = "Pizza 2",
                IngredientIds = new List<int> { 1 },
            };

            // add Pizzas to db
            await _cartService.AddPizzaToCart(1, addPizzaDto);
            await _cartService.AddPizzaToCart(2, addPizza2Dto);

            //------- Test Check
            List<backend.Dto.IngredientStats> ingredientStats = await _statsService.GetIngredientsStats();
            Assert.Equal(2, ingredientStats.Count());
            Assert.Equal("Dough", ingredientStats[0].Name);
            Assert.Equal(2, ingredientStats[0].Count);
            Assert.Equal("Cheese", ingredientStats[1].Name);
            Assert.Equal(1, ingredientStats[1].Count);
        }
    }
}
