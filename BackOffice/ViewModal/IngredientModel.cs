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
        public List<Ingredient> Ingredients;
        public string Test;

        public IngredientModel()
        {
            Ingredients = new List<Ingredient>();
            Ingredients.Add(new Ingredient());
            Test = "Test";
        }
    }
}
