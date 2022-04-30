namespace backend.DataAccess.Interfaces
{
    public interface IPizzaRepository : DataAccess.IRepository<EfModels.Pizza, Dbo.Pizza>
    {
        public List<Dbo.Pizza> GetAllPizzas();
        public Dbo.Pizza GetPizza(long pizzaId);
        public Task<bool> AddPizzaIngredients(int pizzaId, List<int> ingredientsIds);
        public List<Dbo.Ingredient> GetPizzaIngredients(int pizzaId);
    }
}
