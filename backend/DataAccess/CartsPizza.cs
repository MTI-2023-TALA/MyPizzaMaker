﻿using System;
using System.Collections.Generic;

namespace backend.DataAccess
{
    public partial class CartsPizza
    {
        public int PizzaId { get; set; }
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Pizza Pizza { get; set; } = null!;
    }
}
