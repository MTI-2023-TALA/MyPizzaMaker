namespace backend.DataAccess.Interfaces
{
    public interface IPizzaRepository : DataAccess.IRepository<EfModels.Pizza, Dto.Pizza>
    {
    }
}
