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
            try
            {
                var result = _context.Ingredients.ToList();
                return _mapper.Map<List<Dbo.Ingredient>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from the database in GetAllIngredient.", e);
                return null;
            }
        }

        public List<Dbo.Ingredient> GetIngredientWithCategory(string category)
        {
            try
            {
                var result = _context.Ingredients.Where(i => i.Category == category);
                return _mapper.Map<List<Dbo.Ingredient>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from the database in GetIngredientWithCategory.", e);
                return null;
            }
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
                    .GroupBy(p => p.Name)
                    .Select(g => new Dbo.IngredientStats(g.Key, g.Count()))
                    .ToList();
                return ingredientStats;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get data from the database in GetIngredientsStats.", e);
                return null;
            }
        }

        public bool ValidateIngredientCategory(string category)
        {
            try
            {
                List<string> authorized_values = new List<String>()
                {
                    "dough",
                    "base",
                    "cheese",
                    "sauce",
                    "meat",
                    "accompaniments",
                    "drink",
                    "dessert",
                };
                return authorized_values.Contains(category);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured in ValidateIngredientCategory.", e);
                return false;
            }
        }
    }
}
