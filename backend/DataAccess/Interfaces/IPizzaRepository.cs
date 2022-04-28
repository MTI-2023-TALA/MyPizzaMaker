namespace backend.DataAccess.Interfaces
{
    public interface IPizzaRepository : DataAccess.IRepository<EfModels.Pizza, Dto.Pizza>
    {
        public List<Dto.Pizza> GetAllPizzas();
        public Dto.Pizza GetPizza(long pizzaId);
    }
}
