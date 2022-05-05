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
    public class CartService
    {
        public async Task<List<Cart>> LoadCarts()
        {
            string url = Config.BaseWeb + "cart";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Cart> carts = await response.Content.ReadFromJsonAsync<List<Cart>>();
                    return carts;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<bool> PatchCart(int id, PatchCart status)
        {
            string url = Config.BaseWeb + "cart/" + id;

            string json = JsonSerializer.Serialize(status);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PatchAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
