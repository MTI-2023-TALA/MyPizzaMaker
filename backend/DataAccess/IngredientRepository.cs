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
    }
}
