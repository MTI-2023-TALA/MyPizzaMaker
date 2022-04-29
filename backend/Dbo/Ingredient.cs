namespace backend.Dbo
{
    public class Ingredient : IObjectWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImagePath { get; set; }
        public string Category { get; set; }
    }
}
