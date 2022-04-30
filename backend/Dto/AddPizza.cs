namespace backend.Dto
{
    public class AddPizza
    {
        public string Name { get; set; }
        public List<int> IngredientIds { get; set; }
    }
}
