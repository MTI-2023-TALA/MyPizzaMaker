using AutoMapper;
using backend.DataAccess.EfModels;
using backend.DataAccess.Interfaces;

namespace backend.DataAccess
{
    public class IngredientRepository : Repository<EfModels.Ingredient, Dto.Ingredient>, IIngredientRepository
    {
        public IngredientRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Dto.Ingredient> GetAllIngredient()
        {
            var result = _context.Ingredients.ToList();
            return _mapper.Map<List<Dto.Ingredient>>(result);
        }

        public List<Dto.Ingredient> GetIngredientWithCategory(string category)
        {
            var result = _context.Ingredients.Where(i => i.Category == category);
            return _mapper.Map<List<Dto.Ingredient>>(result);
        }
    }
}
