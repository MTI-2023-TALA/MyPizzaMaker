using AutoMapper;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;

namespace backend.DataAccess
{
    public class PizzaRepository : Repository<EfModels.Pizza, Dto.Pizza>, IPizzaRepository
    {
        public PizzaRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Dto.Pizza> GetAllPizzas()
        {
            var result = _context.Pizzas.ToList();
            return _mapper.Map<List<Dto.Pizza>>(result);
        }

        public Dto.Pizza GetPizza(long pizzaId)
        {
            var result = _context.Pizzas.Where(p => p.Id == pizzaId);
            return _mapper.Map<Dto.Pizza>(result);
        }
    }
}
