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
            return Ok(await _ingredientService.GetAllIngredient());
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetIngredientWithCategory(string category)
        {
            return Ok(await _ingredientService.GetByCategory(category));
        }

        [HttpPost]
        public async Task<Ingredient> CreateIngredient([FromBody] CreateIngredient createIngredient)
        {
            return await _ingredientService.CreateIngredient(createIngredient);
        }

        [HttpPatch("{id:int}")]
        public async Task<Ingredient> UpdateIngredient(int id, [FromBody] UpdateIngredient updateIngredient)
        {
            return await _ingredientService.UpdateIngredient(id, updateIngredient);
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> DeleteIngredient(int id)
        {
            return await _ingredientService.DeleteIngredient(id);
        }
    }
}
