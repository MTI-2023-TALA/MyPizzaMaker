using BackOffice.ViewModal;

namespace BackOffice
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = new ShellViewModel();
        }

        public async void OnButtonIngredientClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new IngredientPage(), false);
        }

        public async void OnButtonCommandCLicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CommandPage(), false);
        }

        public async void OnButtonStatsCLicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StatPage(), false);
        }
    }
}