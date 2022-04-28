namespace backend.Dto
{
    public class Cart: IObjectWithId
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
