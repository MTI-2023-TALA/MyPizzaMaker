using AutoMapper;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;

namespace backend.DataAccess
{
    public class PizzaRepository : Repository<EfModels.Pizza, Dbo.Pizza>, IPizzaRepository
    {
        public PizzaRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Dbo.Pizza> GetAllPizzas()
        {
            var result = _context.Pizzas.ToList();
            return _mapper.Map<List<Dbo.Pizza>>(result);
        }

        public Dbo.Pizza GetPizza(long pizzaId)
        {
            var result = _context.Pizzas.Where(p => p.Id == pizzaId);
            return _mapper.Map<Dbo.Pizza>(result);
        }
    }
}
