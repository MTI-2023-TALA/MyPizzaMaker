using backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza.Model
{
    public class PizzaModel
    {
        public int PizzaId;
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
