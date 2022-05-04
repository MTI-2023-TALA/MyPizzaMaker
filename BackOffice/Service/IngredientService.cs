using backend.Dto;
using BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace BackOffice.Service
{
    public class IngredientService
    {
        public async Task<List<Ingredient>> LoadIngredients()
        {
            string url = Config.BaseWeb + "ingredient";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Ingredient> ingredients = await response.Content.ReadFromJsonAsync<List<Ingredient>>();
                    return ingredients;
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task CreateIngredient(CreateIngredient createIngredient)
        {
            string url = Config.BaseWeb + "ingredient";

            string json = JsonSerializer.Serialize(createIngredient);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {

                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
