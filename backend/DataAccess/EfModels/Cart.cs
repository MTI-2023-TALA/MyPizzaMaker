using System;
using System.Collections.Generic;

namespace backend.DataAccess.EfModels
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
