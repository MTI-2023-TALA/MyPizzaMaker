using Microsoft.AspNetCore.Mvc;
using backend.DataAccess.Interfaces;
using backend.Dto;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository, ILogger<IngredientController> logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllIngredient()
        {
            return Ok(_ingredientRepository.GetAllIngredient());
        }

        [HttpGet("{category}")]
        public IActionResult GetIngredientWithCategory(string category)
        {
            return Ok(_ingredientRepository.GetIngredientWithCategory(category));
        }

        [HttpPost]
        public async Task<Ingredient> CreateIngredient([FromBody] CreateIngredient createIngredient)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Category = createIngredient.Category;
            ingredient.Name = createIngredient.Name;
            ingredient.IsAvalaible = createIngredient.IsAvalaible;

            return await _ingredientRepository.Insert(ingredient);
        }

        [HttpPatch("{id:long}")]
        public async Task<Ingredient> UpdateIngredient(int id, [FromBody] UpdateIngredient updateIngredient)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = id;
            ingredient.Name = updateIngredient.Name;
            ingredient.IsAvalaible = (bool)updateIngredient.IsAvalaible;
            ingredient.Category = updateIngredient.Category;

            return await _ingredientRepository.Update(ingredient);
        }

        [HttpDelete("{id:long}")]
        public async Task<bool> DeleteIngredient(long id)
        {
            return await _ingredientRepository.Delete(id);
        }
    }
}
