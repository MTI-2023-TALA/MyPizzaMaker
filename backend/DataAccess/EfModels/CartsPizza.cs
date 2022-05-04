using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class CartsPizza
    {
        public long Id { get; set; }
        public int PizzaId { get; set; }
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Pizza Pizza { get; set; } = null!;
    }
}
