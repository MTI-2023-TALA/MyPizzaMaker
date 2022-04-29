using AutoMapper;
using backend.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Service
{
    public class IngredientService : Interfaces.IIngredientService
    {
        private readonly ILogger<IngredientService> _logger;
        private readonly IIngredientRepository _ingredientRepository;
        protected readonly IMapper _mapper;


        public IngredientService(IIngredientRepository ingredientRepository, ILogger<IngredientService> logger, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.Ingredient>> GetAllIngredient()
        {
            List<Dbo.Ingredient> ingredients = _ingredientRepository.GetAllIngredient();
            return _mapper.Map<List<Dto.Ingredient>>(ingredients);
        }

        public async Task<Dto.Ingredient> GetIngredient(int id)
        {
            Dbo.Ingredient ingredient = await _ingredientRepository.GetOne(id);
            return _mapper.Map<Dto.Ingredient>(ingredient);
        }

        public async Task<List<Dto.Ingredient>> GetByCategory(string category)
        {
            List<Dbo.Ingredient> ingredients = _ingredientRepository.GetIngredientWithCategory(category);
            return _mapper.Map<List<Dto.Ingredient>>(ingredients);
        }

        public async Task<Dto.Ingredient> CreateIngredient(Dto.CreateIngredient createIngredient)
        {
            Dbo.Ingredient ingredient = new Dbo.Ingredient();
            ingredient.Category = createIngredient.Category;
            ingredient.Name = createIngredient.Name;
            ingredient.IsAvailable = createIngredient.IsAvalaible;

            var result = await _ingredientRepository.Insert(ingredient);
            return _mapper.Map<Dto.Ingredient>(result);
        }

        public async Task<Dto.Ingredient> UpdateIngredient(int id, Dto.UpdateIngredient updateIngredient)
        {
            Dbo.Ingredient ingredient = new Dbo.Ingredient();
            ingredient.Id = id;
            ingredient.Name = updateIngredient.Name;
            ingredient.IsAvailable = (bool)updateIngredient.IsAvalaible;
            ingredient.Category = updateIngredient.Category;

            var result = await _ingredientRepository.Update(ingredient);
            return _mapper.Map<Dto.Ingredient>(result);
        }

        public async Task<bool> DeleteIngredient(int id)
        {
            return await _ingredientRepository.Delete(id);
        }
    }
}
