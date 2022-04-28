namespace backend.DataAccess.Interfaces
{
    public interface IIngredientRepository : DataAccess.IRepository<EfModels.Ingredient, Dto.Ingredient>
    {
        public List<Dto.Ingredient> GetAllIngredient();
        public List<Dto.Ingredient> GetIngredientWithCategory(string category);
    }
}
