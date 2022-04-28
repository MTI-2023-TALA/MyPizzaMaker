namespace backend.Dto
{
    public class PizzasIngredient
    {
        public long IngredientId { get; set; }
        public long PizzaId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
