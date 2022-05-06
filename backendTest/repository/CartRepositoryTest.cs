using backend.DataAccess;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace backendTest.repository
{
    public class CartRepositoryTest : IDisposable
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ICartRepository _cartRepository;
        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public CartRepositoryTest()
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

            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IPizzaRepository, PizzaRepository>();
            services.AddTransient<backend.Service.Interfaces.IIngredientService, backend.Service.IngredientService>();
            services.AddTransient<backend.Service.Interfaces.ICartService, backend.Service.CartService>();
            services.AddTransient<backend.Service.Interfaces.IStatsService, backend.Service.StatsService>();
            
            var serviceProvider = services.BuildServiceProvider();
            _pizzaRepository = serviceProvider.GetService<IPizzaRepository>();
            _cartRepository = serviceProvider.GetService<ICartRepository>();
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public async void PopulateDB()
        {
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "in creation",
                Date = DateTime.Today,
            });
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "served",
                Date = DateTime.Today,
            });
        }

        [Fact]
        public void GetAllCartsTest()
        {
            PopulateDB();

            List<backend.Dbo.Cart> carts = _cartRepository.GetAllCarts();
            Assert.Equal(2, carts.Count);
            Assert.Equal("in creation", carts[0].Status);
            Assert.Equal("served", carts[1].Status);
        }

        [Fact]
        public async void GetOneCartTest()
        {
            PopulateDB();

            backend.Dbo.Cart cart = await _cartRepository.GetOne(2);
            Assert.Equal("served", cart.Status);
        }

        [Fact]
        public async void GetOneUnknownCartTest()
        {
            PopulateDB();

            backend.Dbo.Cart cart = await _cartRepository.GetOne(666);
            Assert.Null(cart);
        }

        [Fact]
        public async void GetTodayCartsTest()
        {
            PopulateDB();
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "cancelled",
                Date = new DateTime(2022, 01, 01),
            });

            List<backend.Dbo.Cart> carts = _cartRepository.GetTodayCarts();
            Assert.Equal(2, carts.Count);
            Assert.Equal("in creation", carts[0].Status);
            Assert.Equal("served", carts[1].Status);
        }

        [Fact]
        public async void GetDailyStatsTest()
        {
            PopulateDB();
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "cancelled",
                Date = new DateTime(2022, 01, 01),
            });

            int count = _cartRepository.GetDailyStats();
            Assert.Equal(2, count);
        }

        [Fact]
        public async void GetWeeklyStatsTest()
        {
            DateTime today = DateTime.Today;
            DateTime dateTime1 = today.AddDays(-3);
            DateTime dateTime2 = today.AddDays(-4);
            DateTime dateTime3 = today.AddDays(-8);

            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "in creation",
                Date = dateTime1,
            });
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "served",
                Date = dateTime2,
            });
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "cancelled",
                Date = dateTime3,
            });

            int count = _cartRepository.GetWeeklyStats();
            Assert.Equal(2, count);
        }

        [Fact]
        public async void GetMonthlyStatsTest()
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

            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "in creation",
                Date = dateTime1,
            });
            await _cartRepository.Insert(new backend.Dbo.Cart
            {
                Status = "served",
                Date = dateTime2,
            });

            int count = _cartRepository.GetMonthlyStats();
            Assert.Equal(2, count);
        }

        [Fact]
        public async void AddPizzaToCartTest()
        {
            PopulateDB();
            await _pizzaRepository.Insert(new backend.Dbo.Pizza { Name = "PizzaTest1" });

            bool isPizzaAddedToCart = await _cartRepository.AddPizzaToCart(1, 1);
            Assert.True(isPizzaAddedToCart);
        }

        [Fact]
        public async void GetPizzafromCartTest()
        {
            PopulateDB();
            await _pizzaRepository.Insert(new backend.Dbo.Pizza { Name = "PizzaTest1" });

            await _cartRepository.AddPizzaToCart(1, 1);

            List<backend.Dbo.CartPizzaWithName> pizzas = _cartRepository.GetPizzasfromCart(1);
            Assert.Single(pizzas);
            Assert.Equal("PizzaTest1", pizzas[0].PizzaName);
        }

        [Theory]
        [InlineData("in creation", true)]
        [InlineData("waiting for confirmation", true)]
        [InlineData("in preparation", true)]
        [InlineData("to be collected", true)]
        [InlineData("served", true)]
        [InlineData("cancelled", true)]
        [InlineData("", false)]
        [InlineData("badStatus", false)]
        public void ValidateCartStatusTest(string status, bool expected)
        {
            bool isCorrectStatus = _cartRepository.ValidateCartStatus(status);
            Assert.Equal(expected, isCorrectStatus);
        }
    }
}
