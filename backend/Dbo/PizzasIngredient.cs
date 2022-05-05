namespace backend.Dbo
{
    public class PizzasIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int PizzaId { get; set; }
        
        public virtual Ingredient Ingredient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
