using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontPizza.Service
{
    public class CartService
    {
        public async Task<backend.Dto.Cart> CreateCart()
        {
            var cart = new backend.Dto.CreateCart();
            cart.Status = "in creation";
            cart.Date = DateTime.Now;

            string url = Config.BaseWeb + "cart";
            string json = JsonSerializer.Serialize(cart);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<backend.Dto.Cart>();
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<backend.Dto.CartPizzaIngredient> GetCart(int id)
        {
            string url = Config.BaseWeb + "cart/" + id;

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<backend.Dto.CartPizzaIngredient>();
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<bool> AddPizzaToCart(int id, backend.Dto.AddPizza addPizza)
        {
            string url = Config.BaseWeb + "cart/" + id;

            string json = JsonSerializer.Serialize(addPizza);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            using(HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result == "true";
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<backend.Dto.Cart> UpdateCart(int id, backend.Dto.UpdateCart updateCart)
        {
            string url = Config.BaseWeb + "cart/" + id;
            string json = JsonSerializer.Serialize(updateCart);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PatchAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<backend.Dto.Cart>();
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
