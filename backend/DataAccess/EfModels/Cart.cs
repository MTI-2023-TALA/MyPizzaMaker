using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class Cart
    {
        public Cart()
        {
            CartsPizzas = new HashSet<CartsPizza>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public DateTime Date { get; set; }

        public virtual ICollection<CartsPizza> CartsPizzas { get; set; }
    }
}
