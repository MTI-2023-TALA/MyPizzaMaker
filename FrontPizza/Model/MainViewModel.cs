using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Model
{
    public class MainViewModel
    {
        private IngredientService _ingredientService;
        private CartService _cartService;

        public int Id { get; set; }
        public ObservableRangeCollection<backend.Dto.Ingredient> Ingredients { get; set; }

        public async Task LoadIngredients()
        {
            Ingredients.Clear();

            List<backend.Dto.Ingredient> ingredients = await _ingredientService.LoadIngredients();
            foreach (var ingredient in ingredients)
            {
                if (ingredient.IsAvailable)
                {
                    Ingredients.Add(ingredient);
                }
            }
        }

        public MainViewModel(int Id)
        {
            _ingredientService = new IngredientService();
            _cartService = new CartService();
            this.Id = Id;
            this.Ingredients = new ObservableRangeCollection<backend.Dto.Ingredient>();
        }
    }
}
