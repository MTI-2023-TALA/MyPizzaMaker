namespace FrontPizza;

public partial class IngredientPage : ContentPage
{
	private MainViewModel viewModel => BindingContext as MainViewModel;
	private ISet<int> selectedIngredientIds = new HashSet<int>();
	private int numberOfPizza = 0;

	public IngredientPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override async void OnAppearing()
    {
		base.OnAppearing();
		await viewModel.LoadIngredients();
    }

    private void AddIngredient(object sender, EventArgs e)
    {
		var button = (Button)sender;
		var ingredient = (backend.Dto.Ingredient)button.BindingContext;
		if (button.BackgroundColor == Colors.LightGray)
        {
			selectedIngredientIds.Add(ingredient.Id);
			button.BackgroundColor = Colors.LightGreen;
		} else
        {
			selectedIngredientIds.Remove(ingredient.Id);
			button.BackgroundColor = Colors.LightGray;
        }
	}

    private async void AddPizza(object sender, EventArgs e)
    {
		if (selectedIngredientIds.Count == 0)
        {
			return;
        }

		numberOfPizza += 1;
		string pizzaName = "Pizza " + numberOfPizza;
		List<int> ingredientId = selectedIngredientIds.ToList();

		await viewModel.AddPizza(pizzaName, ingredientId);
		await viewModel.LoadIngredients();
    }

	private async void ConfirmCommand(object sender, EventArgs e)
    {
		if (numberOfPizza == 0)
        {
			return;
        }

		await Navigation.PushAsync(new StatusPage(new StatusViewModel(viewModel.Id)));
    }
}