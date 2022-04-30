namespace backend.Dbo
{
    public class CartPizzaWithName
    {
        public int PizzaId { get; set; }
        public int CartId { get; set; }
        public string PizzaName { get; set; }

        public CartPizzaWithName(int pizzaId, int cartId, string pizzaName)
        {
            PizzaId = pizzaId;
            CartId = cartId;
            PizzaName = pizzaName;
        }
    }
}
