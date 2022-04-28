namespace backend.Dto
{
    public class Pizza : IObjectWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
