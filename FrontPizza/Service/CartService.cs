﻿using System;
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
            cart.Date = DateTime.Now;
            cart.Status = "in creation";

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

        public async Task<backend.Dto.Cart> GetCart(int id)
        {
            string url = Config.BaseWeb + "cart/" + id;

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
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

        public async Task<bool> AddPizzaToCart(int id, backend.Dto.AddPizza addPizza)
        {
            string url = Config.BaseWeb + "cart/" + id;

            string json = JsonSerializer.Serialize(addPizza);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
