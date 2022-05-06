using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Service
{
    public class IngredientService
    {
        public async Task<List<backend.Dto.Ingredient>> LoadIngredients()
        {
            string url = Config.BaseWeb + "ingredient";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<backend.Dto.Ingredient> ingredients = await response.Content.ReadFromJsonAsync<List<backend.Dto.Ingredient>>();
                    return ingredients;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
