using Xunit;
using backend.DataAccess.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using backend.DataAccess.EfModels;
using System;
using Microsoft.Extensions.Logging;
using backend.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace backendTest.repository
{
    public class PizzaRepositoryTest : IDisposable
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly DbContextOptions<myPizzaMakerContext> _options;

        public PizzaRepositoryTest()
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
        }

        public void Dispose()
        {
            var context = new myPizzaMakerContext(_options);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async void TestGetOne()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                context.Pizzas.Add(new Pizza { Name = "Pizza 42" });
                context.Pizzas.Add(new Pizza { Name = "Pizza 43" });
                context.SaveChanges();
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                backend.Dbo.Pizza pizza = await _pizzaRepository.GetOne(1);
                Assert.NotNull(pizza);
                Assert.Equal("Pizza 42", pizza.Name);
                context.SaveChanges();
            }
        }

        [Fact]
        public void TestGetAll()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                context.Pizzas.Add(new Pizza { Name = "Pizza 42" });
                context.Pizzas.Add(new Pizza { Name = "Pizza 43" });
                context.SaveChanges();
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                List<backend.Dbo.Pizza> pizza = _pizzaRepository.GetAllPizzas();
                Assert.Equal(2, pizza.Count());
                context.SaveChanges();
            }
        }

        [Fact]
        public async void TestAddPizza()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                backend.Dbo.Pizza pizzaToInsert = new backend.Dbo.Pizza()
                {
                    Name = "Pizza To Insert"
                };

                backend.Dbo.Pizza insertedPizza = await _pizzaRepository.Insert(pizzaToInsert);
                Assert.NotNull(insertedPizza);
                Assert.Equal("Pizza To Insert", insertedPizza.Name);
            }
        }

        [Fact]
        public async void TestUpdatePizza()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                backend.Dbo.Pizza pizzaToInsert = new backend.Dbo.Pizza()
                {
                    Name = "Pizza To Insert",
                };

                await _pizzaRepository.Insert(pizzaToInsert);
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                backend.Dbo.Pizza pizza = new backend.Dbo.Pizza();
                pizza.Name = "NewPizzaName";
                pizza.Id = 1;

                var updatedPizza = await _pizzaRepository.Update(pizza);
                Assert.NotNull(updatedPizza);
                Assert.Equal("NewPizzaName", updatedPizza.Name);
            }
        }

        [Fact]
        public async void TestDeletePizza()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                backend.Dbo.Pizza pizzaToInsert = new backend.Dbo.Pizza()
                {
                    Name = "Pizza To Insert",
                };

                await _pizzaRepository.Insert(pizzaToInsert);
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                bool isPizzaRemoved = await _pizzaRepository.Delete(1);
                Assert.True(isPizzaRemoved);
            }
        }

        [Fact]
        public void TestGetPizzaIngredients()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                // create pizza
                context.Pizzas.Add(new Pizza { Name = "Pizza 42" });

                // create ingredients
                context.Ingredients.Add(new Ingredient
                {
                    Name = "Tomato",
                    IsAvailable = true,
                    Category = "base",
                });
                context.Ingredients.Add(new Ingredient
                {
                    Name = "Dough Dough",
                    IsAvailable = true,
                    Category = "dough",
                });
                context.Ingredients.Add(new Ingredient
                {
                    Name = "Beef",
                    IsAvailable = true,
                    Category = "meat",
                });

                // add to pizzaIngredients
                context.PizzasIngredients.Add(new PizzasIngredient
                {
                    PizzaId = 1,
                    IngredientId = 1,
                });
                context.PizzasIngredients.Add(new PizzasIngredient
                {
                    PizzaId = 1,
                    IngredientId = 2,
                });
                context.PizzasIngredients.Add(new PizzasIngredient
                {
                    PizzaId = 1,
                    IngredientId = 3,
                });
                context.SaveChanges();
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                List<backend.Dbo.Ingredient> pizzaIngredients = _pizzaRepository.GetPizzaIngredients(1);
                Assert.Equal(3, pizzaIngredients.Count());
                Assert.Equal("Tomato", pizzaIngredients[0].Name);
                Assert.Equal("Dough Dough", pizzaIngredients[1].Name);
                Assert.Equal("Beef", pizzaIngredients[2].Name);
            }
        }

        [Fact]
        public async void TestAddPizzaIngredients()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                // create pizza
                context.Pizzas.Add(new Pizza { Name = "Pizza 42" });

                // create ingredients
                context.Ingredients.Add(new Ingredient
                {
                    Name = "Tomato",
                    IsAvailable = true,
                    Category = "base",
                });
                context.Ingredients.Add(new Ingredient
                {
                    Name = "Dough Dough",
                    IsAvailable = true,
                    Category = "dough",
                });
                context.Ingredients.Add(new Ingredient
                {
                    Name = "Beef",
                    IsAvailable = true,
                    Category = "meat",
                });
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                bool addedPizzaIngredients = await _pizzaRepository.AddPizzaIngredients(1, new List<int>
                {
                    1, 2, 3
                });
                Assert.True(addedPizzaIngredients);
            }
        }
    }
}