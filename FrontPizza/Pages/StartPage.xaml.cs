namespace FrontPizza;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

    private void StartCommand(object sender, EventArgs e)
    {
		// TODO: Get cartId and send it;
		Navigation.PushAsync(new IngredientPage());
    }
}