namespace backend.Dto
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
    }
}
