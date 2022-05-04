using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Service
{
    public static class ServicesExtensions
    {
        public static MauiAppBuilder ConfigureService(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IngredientService>();

            return builder;
        }

    }
}
