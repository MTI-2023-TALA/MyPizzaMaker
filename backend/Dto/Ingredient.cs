namespace backend.Dto
{
    public class Ingredient : IObjectWithId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        public bool IsAvalaible { get; set; }
        public string Category { get; set; }
    }
}
