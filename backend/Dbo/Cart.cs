namespace backend.Dbo
{
    public class Cart: IObjectWithId
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
