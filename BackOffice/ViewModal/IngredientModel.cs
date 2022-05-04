using backend.Dto;
using BackOffice.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.ViewModal
{
    public class IngredientModel
    {
        public ObservableRangeCollection<Ingredient> Ingredients { get; set; }

        public IngredientModel()
        {
            Ingredients = new ObservableRangeCollection<Ingredient>();
            Ingredients.Add(new Ingredient());
        }
    }
}
