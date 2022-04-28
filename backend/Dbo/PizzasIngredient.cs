namespace backend.Dto
{
    public class PizzasIngredient
    {
        public int IngredientId { get; set; }
        public int PizzaId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
