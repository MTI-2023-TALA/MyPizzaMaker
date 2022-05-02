namespace backend.Dbo
{
    public class IngredientStats
    {
        public String Name { get; set; }
        public int Count { get; set; }

        public IngredientStats()
        {

        }

        public IngredientStats(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
