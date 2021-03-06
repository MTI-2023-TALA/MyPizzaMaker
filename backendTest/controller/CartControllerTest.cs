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
using Xunit;

namespace backendTest.controller
{
    public class CartControllerTest : IDisposable
    {
        private readonly CartController _cartController;

        private readonly ICartService _cartService;
        private readonly IIngredientService _ingredientService;

        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public CartControllerTest()
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

            _cartController = new CartController(_cartService, null);
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public async void PopulateDB()
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
                IngredientIds = new List<int> { 1 },
            };
            backend.Dto.AddPizza addPizza2Dto = new backend.Dto.AddPizza
            {
                Name = "Pizza 2",
                IngredientIds = new List<int> { 2 },
            };

            // add Pizzas to db
            await _cartService.AddPizzaToCart(1, addPizzaDto);
            await _cartService.AddPizzaToCart(2, addPizza2Dto);
        }

        [Fact]
        public async void GetAllCartsTest()
        {
            PopulateDB();
            IActionResult res = await _cartController.GetAllCarts();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<backend.Dto.CartPizzaIngredient>>(okResult.Value);
        }

        [Fact]
        public async void GetCartTest()
        {
            PopulateDB();
            IActionResult res = await _cartController.GetCart(1);
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<backend.Dto.CartPizzaIngredient>(okResult.Value);
        }

        [Fact]
        public async void GetCartUnknownCartIdTest()
        {
            PopulateDB();
            IActionResult res = await _cartController.GetCart(42);
            var badResult = res as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("An error occured while retrieving Cart.", badResult.Value);
        }

        [Fact]
        public async void AddPizzaToCartTest()
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

            IActionResult res = await _cartController.AddPizzaToCart(1, new backend.Dto.AddPizza
            {
                Name = "PizzaTest",
                IngredientIds = new List<int> { 1, 2 }
            }); ;
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<bool>(okResult.Value);
        }

        [Fact]
        public async void CreateCartTest()
        {
            IActionResult res = await _cartController.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = DateTime.Today,
            });

            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<backend.Dto.Cart>(okResult.Value);
        }

        [Fact]
        public async void CreateCartBadStatusTest()
        {
            IActionResult res = await _cartController.CreateCart(new backend.Dto.CreateCart
            {
                Status = "badStatus",
                Date = DateTime.Today,
            });

            var badResult = res as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("Please provide a correct status for cart.", badResult.Value);
        }

        [Fact]
        public async void UpdateCartTest()
        {
            PopulateDB();

            IActionResult res = await _cartController.UpdateCart(1, new backend.Dto.UpdateCart
            {
                Status = "served",
            });

            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<backend.Dto.Cart>(okResult.Value);
        }

        [Fact]
        public async void UpdateCartBadStatusTest()
        {
            PopulateDB();

            IActionResult res = await _cartController.UpdateCart(1, new backend.Dto.UpdateCart
            {
                Status = "badStatus",
            });
            var badResult = res as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("Please provide a correct status for cart.", badResult.Value);
        }

        [Fact]
        public async void GetTodayCartsTest()
        {
            // create carts
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "in creation",
                Date = DateTime.Today,
            });
            await _cartService.CreateCart(new backend.Dto.CreateCart
            {
                Status = "served",
                Date = DateTime.Today,
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
                IngredientIds = new List<int> { 1 },
            };
            backend.Dto.AddPizza addPizza2Dto = new backend.Dto.AddPizza
            {
                Name = "Pizza 2",
                IngredientIds = new List<int> { 2 },
            };

            // add Pizzas to db
            await _cartService.AddPizzaToCart(1, addPizzaDto);
            await _cartService.AddPizzaToCart(2, addPizza2Dto);

            // verify
            IActionResult res = await _cartController.GetTodayCarts();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<backend.Dto.Cart>>(okResult.Value);
        }
    }
}
