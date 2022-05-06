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
    public class IngredientControllertTest : IDisposable
    {
        private readonly IngredientController _ingredientController;
        private readonly IIngredientService _ingredientService;

        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public IngredientControllertTest()
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

            _ingredientService = serviceProvider.GetService<IIngredientService>();
            _ingredientController = new IngredientController(_ingredientService, null);
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public async void PopulateDB()
        {
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
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Coca Cola",
                IsAvailable = true,
                Category = "drink",
            });
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Tiramisu",
                IsAvailable = true,
                Category = "dessert",
            });
        }

        [Fact]
        public async void GetAllIngredientTest()
        {
            PopulateDB();

            // test
            IActionResult res = await _ingredientController.GetAllIngredient();
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<backend.Dto.Ingredient>>(okResult.Value);
        }

        [Fact]
        public async void GetIngredientTest()
        {
            PopulateDB();

            // test
            IActionResult res = await _ingredientController.GetIngredient(1);
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<backend.Dto.Ingredient>(okResult.Value);
        }

        [Fact]
        public async void GetUnknownIngredientTest()
        {
            PopulateDB();

            // test
            IActionResult res = await _ingredientController.GetIngredient(666);
            var badResult = res as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("An error occured while retrieving Ingredient.", badResult.Value);
        }

        [Fact]
        public async void GetIngredientWithCategoryTest()
        {
            PopulateDB();   

            // test
            IActionResult res = await _ingredientController.GetIngredientWithCategory("dough");
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<backend.Dto.Ingredient>>(okResult.Value);
        }

        [Fact]
        public async void CreateIngredientTest()
        {
            backend.Dto.CreateIngredient ingredient = new backend.Dto.CreateIngredient
            {
                Name = "Pâte fine",
                IsAvailable = true,
                Category = "dough",
            };

            IActionResult res = await _ingredientController.CreateIngredient(ingredient);
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<backend.Dto.Ingredient>(okResult.Value);
        }

        [Fact]
        public async void CreateIngredientBadCategoryTest()
        {
            backend.Dto.CreateIngredient ingredient = new backend.Dto.CreateIngredient
            {
                Name = "Pâte fine",
                IsAvailable = true,
                Category = "badCategory",
            };

            IActionResult res = await _ingredientController.CreateIngredient(ingredient);
            var badResult = res as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("An error occured while creating Ingredient.", badResult.Value);
        }

        [Fact]
        public async void UpdateIngredientTest()
        {
            PopulateDB();

            backend.Dto.UpdateIngredient updateIngredient = new backend.Dto.UpdateIngredient
            {
                Name = "updatedIngredient",
                IsAvailable = false,
                Category = "drink"
            };

            IActionResult res = await _ingredientController.UpdateIngredient(1, updateIngredient);
            var okResult = res as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<backend.Dto.Ingredient>(okResult.Value);
        }

        [Fact]
        public async void UpdateIngredientBadCategoryTest()
        {
            PopulateDB();

            backend.Dto.UpdateIngredient updateIngredient = new backend.Dto.UpdateIngredient
            {
                Name = "updatedIngredient",
                IsAvailable = false,
                Category = "badCategory"
            };

            IActionResult res = await _ingredientController.UpdateIngredient(1, updateIngredient);
            var badResult = res as BadRequestObjectResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("An error occured while updating Ingredient.", badResult.Value);
        }

        [Fact]
        public async void DeleteIngredientTest()
        {
            PopulateDB();

            bool res = await _ingredientController.DeleteIngredient(1);
            Assert.True(res);
        }

        [Fact]
        public async void DeleteUnknownIngredientTest()
        {
            PopulateDB();

            bool res = await _ingredientController.DeleteIngredient(666);
            Assert.False(res);
        }
    }
}
