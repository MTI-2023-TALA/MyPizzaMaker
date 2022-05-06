namespace backend.Dto
{
    public class CartPizzaIngredient
    {
        public int CartId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public List<Pizza> Pizzas { get; set; } 

        public class Pizza
        {
            public int PizzaId;
            public string Name { get; set; }
            public List<Ingredient> Ingredients { get; set; }
        }

        public class Ingredient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsAvailable { get; set; }
            public string ImagePath { get; set; }
            public string Category { get; set; }
        }
    }
}
