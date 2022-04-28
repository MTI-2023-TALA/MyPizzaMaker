using AutoMapper;
using backend.DataAccess.EfModels;

namespace backend.DataAccess
{
    public class CartRepository : Repository<EfModels.Cart, Dto.Cart>
    {
        public CartRepository(myPizzaMakerContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
