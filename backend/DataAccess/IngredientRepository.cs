using AutoMapper;
using backend.DataAccess.EfModels;

namespace backend.DataAccess
{
    public class IngredientRepository : Repository<EfModels.Ingredient, Dto.Ingredient>
    {
        public IngredientRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
