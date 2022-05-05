using backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Dto
{
    public class Pizza
    {
        public int PizzaId;
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
