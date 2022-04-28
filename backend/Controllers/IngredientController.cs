using Microsoft.AspNetCore.Mvc;
using backend.DataAccess.Interfaces;

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
        public void CreateIngredient()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpPatch("{id:long}")]
        public void UpdateIngredient(long id)
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpDelete("{id:long}")]
        public void DeleteIngredient(long id)
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }
    }
}
