using backend.Dto;
using BackOffice.Helper;
using BackOffice.Service;
using BackOffice.ViewModal;

namespace BackOffice
{
	public partial class CommandPage : ContentPage
	{
		private CartService _cartService;
		public List<Cart> Carts;
		public List<string> Status { get; set; } = new List<string>
		{
			"toto",
			"tata"
		};
		private Dictionary<string, string> statusTrad = new Dictionary<string, string>();
		private CartModel viewModel => BindingContext as CartModel;
		public CommandPage(CartModel vm, CartService cartService)
		{
			InitializeComponent();
			_cartService = cartService;
			BindingContext = vm;
			statusTrad["En attente de confirmation"] = "waiting for confirmation";
			statusTrad["En cours de préparation"] = "in preparation";
			statusTrad["A récupérer"] = "to be collected";
			statusTrad["Servie"] = "served";
			statusTrad["Annulée"] = "cancelled";
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await viewModel.LoadCarts();
		}


        private async void OnStatusChanged(object sender, EventArgs e)
        {
			var picker = (Picker)sender;
			var cart = (Cart)picker.BindingContext;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
			{
				string status = (string)picker.ItemsSource[selectedIndex];
                await _cartService.PatchCart(cart.CartId, new PatchCart{ status = statusTrad[status] });
				await viewModel.LoadCarts();
			}
		}

		private async void UpdateCarts(object sender, EventArgs e)
        {
			await viewModel.LoadCarts();
		}
    }
}