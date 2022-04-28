using Microsoft.AspNetCore.Mvc;

namespace backend.Service.Interfaces
{
    public interface IIngredientService
    {
        public Task<IEnumerable<Dto.Ingredient>> GetAllIngredient();
        public Task<Dto.Ingredient> GetIngredient(int id);

        public Task<List<Dto.Ingredient>> GetByCategory(string category);

        public Task<Dto.Ingredient> CreateIngredient(Dto.CreateIngredient createIngredient);
        public Task<Dto.Ingredient> UpdateIngredient(int id, Dto.UpdateIngredient updateIngredient);
        public Task<bool> DeleteIngredient(int id);
    }
}
