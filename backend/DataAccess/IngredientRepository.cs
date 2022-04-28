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
    }
}
