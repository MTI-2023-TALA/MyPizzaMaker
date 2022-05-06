using backend.Controllers;
using backend.DataAccess;
using backend.DataAccess.EfModels;
using backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backendTest.controller
{
    public class StatsControllerTest : IDisposable
    {
        private readonly StatsController _statsController;

        private readonly ICartService _cartService;
        private readonly IIngredientService _ingredientService;
        private readonly IStatsService _statsService;

        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public StatsControllerTest()
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

            services.AddControllersWithViews();

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

            _statsController = new StatsController(_statsService, null);
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async void GetDailyStatsTest()
        {
            // create carts for today
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

            // test
            IActionResult res = await _statsController.GetDailyStats();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async void GetWeeklyStatsTes()
        {
            // add carts
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

            // test
            IActionResult res = await _statsController.GetWeeklyStats();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async void GetMonthlyStatsTest()
        {
            // add carts
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

            // test
            IActionResult res = await _statsController.GetMonthlyStats();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async void GetIngredientsStatsTest()
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

            // verification
            IActionResult res = await _statsController.GetIngredientsStats();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<backend.Dto.IngredientStats>>(okResult.Value);
        }
    }
}
