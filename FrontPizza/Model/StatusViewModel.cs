using backend.Dto;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Model
{
    public class StatusViewModel : ObservableObject
    {
        public CartService cartService = new CartService();
        public int Id { get; set; }
        private Dictionary<string, string> statusTrad = new Dictionary<string, string>();
        private string status;
        public ObservableRangeCollection<CartPizzaIngredient.Pizza> Pizzas { get; set; }
        public string Status { 
            get { return status; } 
            set { SetProperty(ref status, value); }
        }

        public StatusViewModel(int id)
        {
            Id = id;
            Pizzas = new ObservableRangeCollection<CartPizzaIngredient.Pizza>();
            statusTrad["waiting for confirmation"] = " En attente de confirmation";
            statusTrad["in preparation"] = " En cours de préparation";
            statusTrad["to be collected"] = " A récupérer";
            statusTrad["served"] = " Servie";
            statusTrad["cancelled"] = " Annulée";
        }

        public async Task LoadCart()
        {
            var cart = await cartService.GetCart(Id);
            Status = statusTrad[cart.Status];
            Pizzas.ReplaceRange(cart.Pizzas);
        }
    }
}
