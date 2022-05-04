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
            try
            {
                var result = _context.Pizzas.ToList();
                return _mapper.Map<List<Dbo.Pizza>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from database in GetAllPizzas.", e);
                return null;
            } 
        }

        public async Task<bool> AddPizzaIngredients(int pizzaId, List<int> ingredientsIds)
        {
            try
            {
                foreach (var ingredientId in ingredientsIds)
                {
                    Dbo.PizzasIngredient pizzasIngredient = new Dbo.PizzasIngredient();
                    pizzasIngredient.PizzaId = pizzaId;
                    pizzasIngredient.IngredientId = ingredientId;

                    PizzasIngredient dbPizzasIngredient = _mapper.Map<PizzasIngredient>(pizzasIngredient);
                    _context.PizzasIngredients.Add(dbPizzasIngredient);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to insert data to the database in PizzaIngredients.", e);
                return false;
            }
        }

        public List<Dbo.Ingredient> GetPizzaIngredients(int pizzaId)
        {
            try
            {
                List<Dbo.Ingredient> ingredients = _context.PizzasIngredients.Where(pi => pi.PizzaId == pizzaId)
                    .Join(
                        _context.Ingredients,
                        pi => pi.IngredientId,
                        i => i.Id,
                        (pi, i) => new Dbo.Ingredient(i.Id, i.Name, i.IsAvailable, i.ImagePath, i.Category)
                    ).ToList();
                return ingredients;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from the database in GetPizzaIngredients.", e);
                return null;
            }
        }
    }
}
