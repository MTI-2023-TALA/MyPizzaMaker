using backend.Dto;
using BackOffice.Helper;
using BackOffice.Service;
using BackOffice.ViewModal;

namespace BackOffice
{

	public partial class StatPage : ContentPage
	{
		public int Daily;
		public int Weekly;
		public int Monthly;
		public List<IngredientStat> ingredientStats;

		private StatsModel viewModel => BindingContext as StatsModel;
		public StatPage(StatsModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
			await viewModel.LoadStats();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
			await viewModel.LoadStats();
        }
    }
}