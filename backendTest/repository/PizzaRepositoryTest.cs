using Xunit;
using backend.DataAccess.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using backend.DataAccess.EfModels;
using System;
using Microsoft.Extensions.Logging;
using AutoMapper;
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

            services.AddTransient<IPizzaRepository, PizzaRepository>();
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
        public void TestAddPizza()
        {
            using (var context = new myPizzaMakerContext(_options))
            {
                backend.Dbo.Pizza pizzaToInsert = new backend.Dbo.Pizza()
                {
                    Name = "Pizza To Insert"
                };

                _pizzaRepository.Insert(pizzaToInsert);
            }

            using (var context = new myPizzaMakerContext(_options))
            {
                List<backend.Dbo.Pizza> pizzas = _pizzaRepository.GetAllPizzas();
                Assert.Single(pizzas);
                Assert.Equal("Pizza To Insert", pizzas[0].Name);
            }
        }
    }
}