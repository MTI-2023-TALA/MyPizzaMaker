using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Service
{
    public static class ServicesExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IngredientService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<StatService>();

            return builder;
        }

    }
}
