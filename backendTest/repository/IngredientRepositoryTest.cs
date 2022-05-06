using backend.DataAccess;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backendTest.repository
{
    public class IngredientRepositoryTest : IDisposable
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public IngredientRepositoryTest()
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
            _ingredientRepository = serviceProvider.GetService<IIngredientRepository>();
            _pizzaRepository = serviceProvider.GetService<IPizzaRepository>();
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public async void PopulateDB()
        {
            await _ingredientRepository.Insert(new backend.Dbo.Ingredient
            {
                Name = "Pâte fine",
                IsAvailable = true,
                Category = "dough",
            });
            await _ingredientRepository.Insert(new backend.Dbo.Ingredient
            {
                Name = "Coca Cola",
                IsAvailable = true,
                Category = "drink",
            });
            await _ingredientRepository.Insert(new backend.Dbo.Ingredient
            {
                Name = "Tomato",
                IsAvailable = true,
                Category = "base",
            });
        }

        [Fact]
        public void GetAllIngredientTest()
        {
            PopulateDB();

            List<backend.Dbo.Ingredient> ingredients = _ingredientRepository.GetAllIngredient();
            Assert.NotNull(ingredients);
            Assert.Equal(3, ingredients.Count());
            Assert.Equal("Pâte fine", ingredients[0].Name);
            Assert.Equal("Coca Cola", ingredients[1].Name);
            Assert.Equal("Tomato", ingredients[2].Name);
        }

        [Fact]
        public async void GetOneIngredientTest()
        {
            PopulateDB();

            backend.Dbo.Ingredient ingredient = await _ingredientRepository.GetOne(2);
            Assert.NotNull(ingredient);
            Assert.Equal("Coca Cola", ingredient.Name);
            Assert.Equal("drink", ingredient.Category);
            Assert.True(ingredient.IsAvailable);
        }

        [Fact]
        public async void GetOneUnknownIngredientTest()
        {
            PopulateDB();

            backend.Dbo.Ingredient ingredient = await _ingredientRepository.GetOne(666);
            Assert.Null(ingredient);
        }

        [Fact]
        public void GetIngredientWithCategoryTest()
        {
            PopulateDB();

            List<backend.Dbo.Ingredient> ingredients = _ingredientRepository.GetIngredientWithCategory("base");
            Assert.Single(ingredients);
            Assert.Equal("Tomato", ingredients[0].Name);
        }

        [Fact]
        public void GetIngredientWithBadCategoryTest()
        {
            PopulateDB();

            List<backend.Dbo.Ingredient> ingredients = _ingredientRepository.GetIngredientWithCategory("category");
            Assert.Empty(ingredients);
        }

        [Fact]
        public async void GetIngredientsStatsTest()
        {
            PopulateDB();
            await _pizzaRepository.Insert(new backend.Dbo.Pizza{ Name = "PizzaTest 1"});
            await _pizzaRepository.Insert(new backend.Dbo.Pizza{ Name = "PizzaTest 2"});

            // add ingredients to pizzaIngredients
            await _pizzaRepository.AddPizzaIngredients(1, new List<int> { 1, 2, 3 });
            await _pizzaRepository.AddPizzaIngredients(2, new List<int> { 1 });

            List<backend.Dbo.IngredientStats> ingredientStats = _ingredientRepository.GetIngredientsStats();

            // assert
            Assert.Equal(3, ingredientStats.Count());
            Assert.Equal("Pâte fine", ingredientStats[0].Name);
            Assert.Equal(2, ingredientStats[0].Count);
            Assert.Equal("Coca Cola", ingredientStats[1].Name);
            Assert.Equal(1, ingredientStats[1].Count);
            Assert.Equal("Tomato", ingredientStats[2].Name);
            Assert.Equal(1, ingredientStats[2].Count);
        }

        [Theory]
        [InlineData("dough", true)]
        [InlineData("base", true)]
        [InlineData("cheese", true)]
        [InlineData("sauce", true)]
        [InlineData("meat", true)]
        [InlineData("accompaniments", true)]
        [InlineData("drink", true)]
        [InlineData("dessert", true)]
        [InlineData("", false)]
        [InlineData("badCategory", false)]
        public void ValidateIngredientCategoryTest(string category, bool expected)
        {
            bool isCorrectCategory = _ingredientRepository.ValidateIngredientCategory(category);
            Assert.Equal(expected, isCorrectCategory);
        }
    }
}
