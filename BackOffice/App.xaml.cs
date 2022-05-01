namespace BackOffice
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AppShell());

            Routing.RegisterRoute(nameof(IngredientPage), typeof(IngredientPage));
            Routing.RegisterRoute(nameof(CommandPage), typeof(CommandPage));
            Routing.RegisterRoute(nameof(StatPage), typeof(StatPage));
        }
    }
}