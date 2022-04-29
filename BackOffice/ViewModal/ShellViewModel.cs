using BackOffice.Models;
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
            Ingredient = new AppSection() { Title = "Ingrédient", Icon = "ingredient.png", TargetType = typeof(IngredientPage) };
            Command = new AppSection() { Title = "Commandes", Icon = "command.png", TargetType = typeof(CommandPage) };
            Stats = new AppSection() { Title = "Statistiques", Icon = "stats.png", TargetType = typeof(StatPage) };
        }
    }
}
