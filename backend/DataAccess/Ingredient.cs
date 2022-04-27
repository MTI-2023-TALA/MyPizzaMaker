using System;
using System.Collections.Generic;

namespace backend.DataAccess
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImagePath { get; set; }
        public bool IsAvailable { get; set; }
        public string Category { get; set; } = null!;
    }
}
