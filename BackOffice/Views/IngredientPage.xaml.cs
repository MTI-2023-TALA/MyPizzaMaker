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
            this._ingredientService = new IngredientService();
            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            this.Ingredients = await this._ingredientService.LoadIngredients();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine(Ingredients.Count);
            CreateIngredient createIngredient = new CreateIngredient();
            createIngredient.Name = this.name.Text;
            createIngredient.Category = CategoryHelper.TranslateCategory((string)this.category.ItemsSource[this.category.SelectedIndex]);
            createIngredient.IsAvailable = this.isAvailable.IsChecked;

            this.name.Text = "";
            await this._ingredientService.CreateIngredient(createIngredient);
            this.Ingredients = await this._ingredientService.LoadIngredients();
        }
    }
}