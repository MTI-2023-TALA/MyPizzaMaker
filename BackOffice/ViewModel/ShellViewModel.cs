using BackOffice.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.ViewModal
{
    public class ShellViewModel
    {
        public AppSection Ingredient { get; set; }
        public AppSection Command { get; set; }
        public AppSection Stats { get; set; }

        public ShellViewModel()
        {
            Ingredient = new AppSection() { Title = "Ingrédient", Icon = "bag.png", TargetType = typeof(IngredientPage) };
            Command = new AppSection() { Title = "Commandes", Icon = "cart.png", TargetType = typeof(CommandPage) };
            Stats = new AppSection() { Title = "Statistiques", Icon = "bar_chart.png", TargetType = typeof(StatPage) };
        }
    }
}
