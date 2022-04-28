namespace backend.Dto
{
    public class CartsPizza
    {
        public long PizzaId { get; set; }
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
