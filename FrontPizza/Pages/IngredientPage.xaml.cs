namespace FrontPizza;

public partial class IngredientPage : ContentPage
{
	public IngredientPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}