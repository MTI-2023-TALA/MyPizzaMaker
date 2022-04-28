using AutoMapper;
using backend.DataAccess.EfModels;

namespace backend.DataAccess
{
    public class PizzaRepository : Repository<EfModels.Pizza, Dto.Pizza>
    {
        public PizzaRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
