using backend.DataAccess;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;
using backend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backendTest.services
{
    public class CartServiceTest : IDisposable
    {
        private readonly ICartService _cartService;
        private readonly IIngredientService _ingredientService;

        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public CartServiceTest()
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
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async void GetAllCartsTest()
        {
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

            // create pizzas

        }

        [Fact]
        public async void TestAddCart()
        {
            backend.Dto.Cart addedCart = await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = new DateTime(2022, 12, 20),
            });
            Assert.Equal("in creation", addedCart.Status);
            Assert.Equal(new DateTime(2022, 12, 20), addedCart.Date);
        }

        [Fact]
        public async void TestBadStatusAddCart()
        {
            backend.Dto.Cart addedCart = await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "badStatusForOrder",
                Date = new DateTime(2022, 12, 20),
            });
            Assert.Null(addedCart);
        }

        [Fact]
        public async void TestUpdateCart()
        {
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = new DateTime(2022, 12, 20),
            });

            backend.Dto.Cart updatedCart = await (_cartService.UpdateCart(1, new backend.Dto.UpdateCart
            {
                Status = "served",
            }));
            Assert.Equal("served", updatedCart.Status);
        }

        [Fact]
        public async void TestBadStatusUpdateCart()
        {
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = new DateTime(2022, 12, 20),
            });

            backend.Dto.Cart updatedCart = await (_cartService.UpdateCart(1, new backend.Dto.UpdateCart
            {
                Status = "notCorrectStatusOrder",
            }));
            Assert.Null(updatedCart);
        }

        [Fact]
        public async void GetTodayCarts()
        {
            DateTime dateTime = DateTime.Now.AddHours(-1);
            DateTime dateTime2 = DateTime.Now.AddHours(-2);
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = dateTime,
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "served",
                Date = dateTime2,
            });

            var todayCarts = await _cartService.GetTodayCarts();
            Assert.Equal(2, todayCarts.Count());
            Assert.Equal(dateTime, todayCarts[0].Date);
            Assert.Equal("in creation", todayCarts[0].Status);
            Assert.Equal(dateTime2, todayCarts[1].Date);
            Assert.Equal("served", todayCarts[1].Status);
        }

        [Fact]
        public async void AddPizzaToCartTest()
        {
            // create cart
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = new DateTime(2022, 12, 20),
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

            // create Dto to send
            backend.Dto.AddPizza addPizzaDto = new backend.Dto.AddPizza
            {
                Name = "Pizza Test",
                IngredientIds = new List<int> { 1, 2 },
            };

            bool isPizzaAdded = await _cartService.AddPizzaToCart(1, addPizzaDto);
            Assert.True(isPizzaAdded);
        }
    }
}
