using Microsoft.AspNetCore.Mvc;
using backend.DataAccess.Interfaces;
using backend.Service.Interfaces;
using backend.Dto;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService, ILogger<IngredientController> logger)
        {
            _ingredientService = ingredientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredient()
        {
            var ingredients = await _ingredientService.GetAllIngredient();
            if (ingredients == null)
            {
                return BadRequest("An error occured while retrieving Ingredients.");
            }
            return Ok(ingredients);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredient(id);
            if (ingredient == null)
            {
                return BadRequest("An error occured while retrieving Ingredient.");
            }
            return Ok(ingredient);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetIngredientWithCategory(string category)
        {
            var ingredients = await _ingredientService.GetByCategory(category);
            if (ingredients == null)
            {
                return BadRequest("An error occured while retrieving IngredientsWithCategory.");
            }
            return Ok(ingredients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredient createIngredient)
        {
            var createdIngredient = await _ingredientService.CreateIngredient(createIngredient);
            if (createdIngredient == null)
            {
                return BadRequest("An error occured while creating Ingredient.");
            }
            return Ok(createdIngredient);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] UpdateIngredient updateIngredient)
        {
            var updatedIngredient = await _ingredientService.UpdateIngredient(id, updateIngredient);
            if (updatedIngredient == null)
            {
                return BadRequest("An error occured while updating Ingredient.");
            }
            return Ok(updatedIngredient);
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteIngredient(int id)
        {
            return await _ingredientService.DeleteIngredient(id);
        }
    }
}
