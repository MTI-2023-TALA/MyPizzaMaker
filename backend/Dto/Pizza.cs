namespace backend.Dto
{
    public class Pizza : IObjectWithId
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
