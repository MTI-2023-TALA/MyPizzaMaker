using backend.Dto;
using BackOffice.Service;

namespace BackOffice
{
    public partial class IngredientPage : ContentPage
    {
        private IngredientService _ingredientService;

        public IngredientPage()
        {
            InitializeComponent();

            _ingredientService = new IngredientService();
        }

        public async Task loadIngredient()
        {
            await _ingredientService.LoadIngredients();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await this.loadIngredient();

            CreateIngredient createIngredient = new CreateIngredient();
            createIngredient.Name = this.name.Text;
            createIngredient.Category = (string)this.category.ItemsSource[this.category.SelectedIndex];
            createIngredient.IsAvailable = this.isAvailable.IsChecked;

            await this._ingredientService.CreateIngredient(createIngredient);
        }
    }
}