using backend.Dto;
using BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.ViewModal
{
    internal class IngredientModel
    {
        public AppSection Ingredient { get; set; }

        public IngredientModel()
        {
            Ingredient = new AppSection() { Title = "Ingrédient", Icon = "ingredient.png", TargetType = typeof(IngredientPage) };
        }
    }
}
