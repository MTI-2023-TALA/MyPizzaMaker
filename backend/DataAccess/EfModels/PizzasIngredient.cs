using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class PizzasIngredient
    {
        public long Id { get; set; }
        public int IngredientId { get; set; }
        public int PizzaId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Pizza Pizza { get; set; } = null!;
    }
}
