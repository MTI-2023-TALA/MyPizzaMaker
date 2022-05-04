using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class Pizza
    {
        public Pizza()
        {
            CartsPizzas = new HashSet<CartsPizza>();
            PizzasIngredients = new HashSet<PizzasIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CartsPizza> CartsPizzas { get; set; }
        public virtual ICollection<PizzasIngredient> PizzasIngredients { get; set; }
    }
}
