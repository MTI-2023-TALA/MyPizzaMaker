using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.ViewModal
{
    public static class ViewModelExtensions
    {
        public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IngredientModel>();
            builder.Services.AddTransient<CartModel>();
            builder.Services.AddSingleton<ShellViewModel>();
            return builder;
        }

    }
}
