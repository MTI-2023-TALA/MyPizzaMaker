namespace FrontPizza;

public partial class StartPage : ContentPage
{
	private CartService _cartService;
	public StartPage()
	{
		InitializeComponent();
		_cartService = new CartService();
	}

    private async void StartCommand(object sender, EventArgs e)
    {
		backend.Dto.Cart cart = await _cartService.CreateCart();
		await Navigation.PushAsync(new IngredientPage(new MainViewModel(cart.Id)));
    }
}