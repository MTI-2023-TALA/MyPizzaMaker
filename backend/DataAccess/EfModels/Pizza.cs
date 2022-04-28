using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
