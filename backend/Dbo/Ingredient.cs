namespace backend.Dbo
{
    public class Ingredient : IObjectWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImagePath { get; set; }
        public string Category { get; set; }

        public Ingredient(int id, string name, bool isAvailable, string? imagePath, string category)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
            ImagePath = imagePath;
            Category = category;
        }

        public Ingredient()
        {}
    }
}
