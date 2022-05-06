namespace FrontPizza;

public partial class IngredientPage : ContentPage
{
	private MainViewModel viewModel => BindingContext as MainViewModel;
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
}