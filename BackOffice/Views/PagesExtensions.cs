using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Views
{
    public static class PagesExtensions
    {
        public static MauiAppBuilder ConfigureViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<CommandPage>();
            builder.Services.AddTransient<IngredientPage>();
            builder.Services.AddTransient<StatPage>();


            return builder;
        }

    }
}
