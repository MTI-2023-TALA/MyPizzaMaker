using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> _logger;

        public IngredientController(ILogger<IngredientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void GetAllIngredient()
        {
            _logger.Log(LogLevel.Information, "Not implemented");
        }

        [HttpGet("{category}")]
        public void GetIngredientWithCategory(string catagory)
        {
            _logger.Log(LogLevel.Information, "Not implemented");
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
