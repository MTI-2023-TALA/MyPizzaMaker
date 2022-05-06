namespace FrontPizza
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AppShell());

            App.Current.UserAppTheme = AppTheme.Light;

            Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
            Routing.RegisterRoute(nameof(IngredientPage), typeof(IngredientPage));
            Routing.RegisterRoute(nameof(ConfirmationPage), typeof(ConfirmationPage));
            Routing.RegisterRoute(nameof(StatusPage), typeof(StatusPage));
        }
    }
}