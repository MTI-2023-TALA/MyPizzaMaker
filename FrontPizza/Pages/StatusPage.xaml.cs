namespace FrontPizza;

public partial class StatusPage : ContentPage
{
	public string Status;

	private StatusViewModel viewModel => BindingContext as StatusViewModel;
	public StatusPage(StatusViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await viewModel.LoadCart();
    }

    private async void Reload(object sender, EventArgs e)
    {
		await viewModel.LoadCart();
    }

	private async void Restart(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new StartPage());
	}
}