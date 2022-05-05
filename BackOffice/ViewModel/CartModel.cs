using backend.Dto;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.ViewModal
{
    public class CartModel
    {
        public CartService _cartService;
        public ObservableRangeCollection<Cart>InCreationCarts { get; set; }
        public ObservableRangeCollection<Cart> WaitingCarts { get; set; }
        public ObservableRangeCollection<Cart> PreparationCarts { get; set; }
        public ObservableRangeCollection<Cart> CollectedCarts { get; set; }
        public ObservableRangeCollection<Cart> ServedCarts { get; set; }
        public ObservableRangeCollection<Cart> CancelledCarts { get; set; }

        public async Task LoadCarts()
        {
            InCreationCarts.Clear();
            WaitingCarts.Clear();
            PreparationCarts.Clear();
            CollectedCarts.Clear();
            ServedCarts.Clear();
            CancelledCarts.Clear();

            List<Cart> Carts = await _cartService.LoadCarts();

            foreach (var cart in Carts)
            {
                if (cart.Status == "in creation")
                {
                    InCreationCarts.Add(cart);
                }
                else if (cart.Status == "waiting for confirmation")
                {
                    WaitingCarts.Add(cart);
                }
                else if (cart.Status == "in preparation")
                {
                    PreparationCarts.Add(cart);
                }
                else if (cart.Status == "to be collected")
                {
                    CollectedCarts.Add(cart);
                }
                else if (cart.Status == "served")
                {
                    ServedCarts.Add(cart);
                }
                else if (cart.Status == "cancelled")
                {
                    CancelledCarts.Add(cart);
                }

            }
        }

        public CartModel(CartService cartService)
        {
            _cartService = cartService;
            InCreationCarts = new ObservableRangeCollection<Cart>();
            WaitingCarts = new ObservableRangeCollection<Cart>();
            PreparationCarts = new ObservableRangeCollection<Cart>();
            CollectedCarts = new ObservableRangeCollection<Cart>();
            ServedCarts = new ObservableRangeCollection<Cart>();
            CancelledCarts = new ObservableRangeCollection<Cart>();
        }
    }
}
