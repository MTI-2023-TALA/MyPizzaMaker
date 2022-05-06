using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Service
{
    public static class ServiceExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<IngredientService>();

            return builder;
        }
    }
}
