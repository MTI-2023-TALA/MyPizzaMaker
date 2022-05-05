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
        private IngredientModel viewModel => BindingContext as IngredientModel;


        public IngredientPage(IngredientModel vm, IngredientService ingredientService)
        {
            InitializeComponent();
            _ingredientService = ingredientService;
            BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadIngredients();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            CreateIngredient createIngredient = new CreateIngredient();
            createIngredient.Name = name.Text;
            createIngredient.Category = CategoryHelper.TranslateCategory((string)category.ItemsSource[category.SelectedIndex]);
            createIngredient.IsAvailable = isAvailable.IsChecked;

            name.Text = "";
            await _ingredientService.CreateIngredient(createIngredient);
            await viewModel.LoadIngredients();
        }
    }
}