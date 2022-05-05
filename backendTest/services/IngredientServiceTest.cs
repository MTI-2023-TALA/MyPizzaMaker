using backend.DataAccess;
using backend.DataAccess.EfModels;
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
    public class IngredientServiceTest : IDisposable
    {
        private readonly ICartService _cartService;
        private readonly IIngredientService _ingredientService;
        private readonly IStatsService _statsService;

        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public IngredientServiceTest()
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
                Name = "Boeuf",
                IsAvailable = true,
                Category = "meat",
            });
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Coca Cola",
                IsAvailable = true,
                Category = "drink",
            });
        }

        [Fact]
        public async void GetAllIngredientTest()
        {
            // put elements in database
            PopulateDB();

            List<backend.Dto.Ingredient> ingredients = await _ingredientService.GetAllIngredient();
            Assert.Equal(4, ingredients.Count());
            Assert.Equal("dough", ingredients[0].Category);
            Assert.Equal("cheese", ingredients[1].Category);
            Assert.Equal("meat", ingredients[2].Category);
            Assert.Equal("drink", ingredients[3].Category);
        }

        [Fact]
        public async void GetIngredientTest()
        {
            // put elements in database
            PopulateDB();

            // checks
            backend.Dto.Ingredient ingredient = await _ingredientService.GetIngredient(1);
            Assert.NotNull(ingredient);
            Assert.Equal("Dough", ingredient.Name);
            Assert.True(ingredient.IsAvailable);
            Assert.Equal("dough", ingredient.Category);
        }

        [Fact]
        public async void GetIngredientsByCategory()
        {
            // put elements in database
            PopulateDB();
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Ice Tea",
                IsAvailable = true,
                Category = "drink",
            });
            await _ingredientService.CreateIngredient(new backend.Dto.CreateIngredient
            {
                Name = "Fanta",
                IsAvailable = true,
                Category = "drink",
            });

            // checks
            List<backend.Dto.Ingredient> ingredients = await _ingredientService.GetByCategory("drink");
            Assert.Equal(3, ingredients.Count());
            Assert.Equal("Coca Cola", ingredients[0].Name);
            Assert.Equal("Ice Tea", ingredients[1].Name);
            Assert.Equal("Fanta", ingredients[2].Name);
        }

        [Fact]
        public async void CreateIngredientTest()
        {
            backend.Dto.Ingredient createdIngredient = await _ingredientService.CreateIngredient(
                new backend.Dto.CreateIngredient
                {
                    Name = "Boeuf",
                    IsAvailable = true,
                    Category = "meat",
                });
            Assert.NotNull(createdIngredient);
            Assert.Equal("Boeuf", createdIngredient.Name);
            Assert.True(createdIngredient.IsAvailable);
            Assert.Equal("meat", createdIngredient.Category);
        }

        [Fact]
        public async void CreateIngredientBadCategoryTest()
        {
            backend.Dto.Ingredient createdIngredient = await _ingredientService.CreateIngredient(
                new backend.Dto.CreateIngredient
                {
                    Name = "Boeuf",
                    IsAvailable = true,
                    Category = "badCategory",
                });
            Assert.Null(createdIngredient);
        }

        [Fact]
        public async void UpdateIngredientTest()
        {
            await _ingredientService.CreateIngredient(
                new backend.Dto.CreateIngredient
                {
                    Name = "Boeuf",
                    IsAvailable = true,
                    Category = "meat",
                });
            var updatedIngredient = await _ingredientService.UpdateIngredient(1,
                new backend.Dto.UpdateIngredient
                {
                    Name = "Boeuf drink",
                    IsAvailable = false,
                    Category = "drink",
                });

            Assert.NotNull(updatedIngredient);
            Assert.Equal("Boeuf drink", updatedIngredient.Name);
            Assert.Equal("drink", updatedIngredient.Category);
            Assert.False(updatedIngredient.IsAvailable);
        }

        [Fact]
        public async void UpdateIngredientBadCategoryTest()
        {
            // add elements in database
            await _ingredientService.CreateIngredient(
                new backend.Dto.CreateIngredient
                {
                    Name = "Boeuf",
                    IsAvailable = true,
                    Category = "meat",
                });
            var updatedIngredient = await _ingredientService.UpdateIngredient(1,
                new backend.Dto.UpdateIngredient
                {
                    Name = "Boeuf drink",
                    IsAvailable = false,
                    Category = "drinkBadCategory",
                });

            // verification
            Assert.Null(updatedIngredient);
        }

        [Fact]
        public async void DeleteIngredientTest()
        {
            await _ingredientService.CreateIngredient(
                new backend.Dto.CreateIngredient
                {
                    Name = "Boeuf",
                    IsAvailable = true,
                    Category = "meat",
                });

            bool isDeletedIngredient = await _ingredientService.DeleteIngredient(1);
            Assert.True(isDeletedIngredient);
        }

        [Fact]
        public async void DeleteIngredientNotExistingIngredientTest()
        {
            // add element in database
            await _ingredientService.CreateIngredient(
                new backend.Dto.CreateIngredient
                {
                    Name = "Boeuf",
                    IsAvailable = true,
                    Category = "meat",
                });

            // verif - delete non existing Id
            bool isDeletedIngredient = await _ingredientService.DeleteIngredient(3);
            Assert.False(isDeletedIngredient);
        }
    }
}
