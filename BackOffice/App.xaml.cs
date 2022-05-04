namespace BackOffice
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Helper.ApiHelper.InitializeClient();

            MainPage = new AppShell();

            App.Current.UserAppTheme = AppTheme.Light;

            Routing.RegisterRoute(nameof(IngredientPage), typeof(IngredientPage));
            Routing.RegisterRoute(nameof(CommandPage), typeof(CommandPage));
            Routing.RegisterRoute(nameof(StatPage), typeof(StatPage));
        }
    }
}