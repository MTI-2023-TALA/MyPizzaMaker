namespace backend.DataAccess.Interfaces
{
    public interface IIngredientRepository : DataAccess.IRepository<EfModels.Ingredient, Dbo.Ingredient>
    {
        public List<Dbo.Ingredient> GetAllIngredient();
        public List<Dbo.Ingredient> GetIngredientWithCategory(string category);
        public List<Dbo.IngredientStats> GetIngredientsStats();
    }
}
