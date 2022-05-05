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
        public IngredientService _ingredientService;
        public ObservableRangeCollection<Ingredient> DoughIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> BaseIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> CheeseIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> SauceIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> MeatIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> AccompanimentsIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> DrinkIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> DessertIngredients { get; set; }

        public Command DeleteIngredient { get; set; }

        public async Task LoadIngredients()
        {
            DoughIngredients.Clear();
            BaseIngredients.Clear();
            CheeseIngredients.Clear();
            SauceIngredients.Clear();
            MeatIngredients.Clear();
            AccompanimentsIngredients.Clear();
            DrinkIngredients.Clear();
            DessertIngredients.Clear();


            List<Ingredient> Ingredients = await _ingredientService.LoadIngredients();

            foreach (var ingredient in Ingredients)
            {
                if (ingredient.Category == "dough")
                {
                    DoughIngredients.Add(ingredient);
                }
                if (ingredient.Category == "base")
                {
                    BaseIngredients.Add(ingredient);
                }
                else if (ingredient.Category == "cheese")
                {
                    CheeseIngredients.Add(ingredient);
                }
                else if (ingredient.Category == "sauce")
                {
                    SauceIngredients.Add(ingredient);
                }
                else if (ingredient.Category == "meat")
                {
                    MeatIngredients.Add(ingredient);
                }
                else if (ingredient.Category == "accompaniments")
                {
                    AccompanimentsIngredients.Add(ingredient);
                }
                else if (ingredient.Category == "drink")
                {
                    DrinkIngredients.Add(ingredient);
                }
                else if (ingredient.Category == "dessert")
                {
                    DessertIngredients.Add(ingredient);
                }
            }
        }

        public IngredientModel(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
            DoughIngredients = new ObservableRangeCollection<Ingredient>();
            BaseIngredients = new ObservableRangeCollection<Ingredient>();
            SauceIngredients = new ObservableRangeCollection<Ingredient>();
            CheeseIngredients = new ObservableRangeCollection<Ingredient>();
            MeatIngredients = new ObservableRangeCollection<Ingredient>();
            AccompanimentsIngredients = new ObservableRangeCollection<Ingredient>();
            DrinkIngredients = new ObservableRangeCollection<Ingredient>();
            DessertIngredients = new ObservableRangeCollection<Ingredient>();
        }
    }
}
