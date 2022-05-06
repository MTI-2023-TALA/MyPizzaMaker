using backend.Dto;
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

        public ObservableRangeCollection<Ingredient> DoughIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> BaseIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> CheeseIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> SauceIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> MeatIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> AccompanimentsIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> DrinkIngredients { get; set; }
        public ObservableRangeCollection<Ingredient> DessertIngredients { get; set; }

        public int Id { get; set; }

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

            List<Ingredient> ingredients = await _ingredientService.LoadIngredients();
            foreach (var ingredient in ingredients)
            {
                if (ingredient.IsAvailable)
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
        }

        public async Task AddPizza(string name, List<int> IngredientIds)
        {
            backend.Dto.AddPizza pizza = new backend.Dto.AddPizza
            {
                IngredientIds = IngredientIds,
                Name = name
            };

            await _cartService.AddPizzaToCart(Id, pizza);
        }

        public async Task ConfirmCommand()
        {
            backend.Dto.UpdateCart updateCart = new backend.Dto.UpdateCart
            {
                Status = "waiting for confirmation"
            };

            backend.Dto.Cart cart = await _cartService.UpdateCart(Id, updateCart);
        }

        public MainViewModel(int Id)
        {
            _ingredientService = new IngredientService();
            _cartService = new CartService();
            DoughIngredients = new ObservableRangeCollection<Ingredient>();
            BaseIngredients = new ObservableRangeCollection<Ingredient>();
            SauceIngredients = new ObservableRangeCollection<Ingredient>();
            CheeseIngredients = new ObservableRangeCollection<Ingredient>();
            MeatIngredients = new ObservableRangeCollection<Ingredient>();
            AccompanimentsIngredients = new ObservableRangeCollection<Ingredient>();
            DrinkIngredients = new ObservableRangeCollection<Ingredient>();
            DessertIngredients = new ObservableRangeCollection<Ingredient>();
            this.Id = Id;
        }
    }
}
