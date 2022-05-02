using AutoMapper;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;

namespace backend.DataAccess
{
    public class IngredientRepository : Repository<EfModels.Ingredient, Dbo.Ingredient>, IIngredientRepository
    {
        public IngredientRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Dbo.Ingredient> GetAllIngredient()
        {
            var result = _context.Ingredients.ToList();
            return _mapper.Map<List<Dbo.Ingredient>>(result);
        }

        public List<Dbo.Ingredient> GetIngredientWithCategory(string category)
        {
            var result = _context.Ingredients.Where(i => i.Category == category);
            return _mapper.Map<List<Dbo.Ingredient>>(result);
        }

        public List<Dbo.IngredientStats> GetIngredientsStats()
        {
            try
            {
                List<Dbo.IngredientStats> ingredientStats = _context
                    .PizzasIngredients
                    .Join(
                        _context.Ingredients,
                        pi => pi.IngredientId,
                        i => i.Id,
                        (pi, i) => new
                        {
                            IngredientId = i.Id,
                            Name = i.Name,
                            PizzaId = pi.PizzaId
                        }
                    )
                    .GroupBy(
                        p => p.Name)
                    .Select(g => new Dbo.IngredientStats(g.Key, g.Count())).ToList();
                return ingredientStats;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from the database in GetIngredientsStats", e);
                return null;
            }
        }
    }
}
