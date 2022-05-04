namespace backend.Dbo
{
    public class CartsPizza
    {
        public long Id { get; set; }
        public int PizzaId { get; set; }
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
