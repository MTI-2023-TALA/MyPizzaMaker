using backend.Dto;
using BackOffice.Helper;
using BackOffice.Service;
using BackOffice.ViewModal;

namespace BackOffice
{
    public partial class IngredientPage : ContentPage
    {
        private IngredientService _ingredientService;
        public List<Ingredient> Ingredients;

        public IngredientPage()
        {
            InitializeComponent();
            _ingredientService = new IngredientService();
            BindingContext = new IngredientModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Ingredients = await _ingredientService.LoadIngredients();
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine(Ingredients.Count);
            CreateIngredient createIngredient = new CreateIngredient();
            createIngredient.Name = name.Text;
            createIngredient.Category = CategoryHelper.TranslateCategory((string)category.ItemsSource[category.SelectedIndex]);
            createIngredient.IsAvailable = isAvailable.IsChecked;

            name.Text = "";
            await _ingredientService.CreateIngredient(createIngredient);
            Ingredients = await _ingredientService.LoadIngredients();
        }
    }
}