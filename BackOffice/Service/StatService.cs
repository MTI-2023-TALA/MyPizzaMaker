using backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Service
{
    public class StatService
    {
        public async Task<int> GetStats(string when)
        {
            string url = Config.BaseWeb + "stats/" + when;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    int stat = await response.Content.ReadFromJsonAsync<int>();
                    return stat;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<IngredientStat>> GetIngredientsStats()
        {
            string url = Config.BaseWeb + "stats/ingredients";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stats = await response.Content.ReadFromJsonAsync<List<IngredientStat>>();
                    return stats;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
