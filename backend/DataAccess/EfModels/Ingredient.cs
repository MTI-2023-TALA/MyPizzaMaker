using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PizzasIngredients = new HashSet<PizzasIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImagePath { get; set; }
        public bool IsAvailable { get; set; }
        public string Category { get; set; } = null!;

        public virtual ICollection<PizzasIngredient> PizzasIngredients { get; set; }
    }
}
