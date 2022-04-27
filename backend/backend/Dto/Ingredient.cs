namespace backend.Dto
{
    public class Ingredient : IObjectWithId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public bool IsAvalaible { get; set; }
        public string Categorie { get; set; }
    }
}
