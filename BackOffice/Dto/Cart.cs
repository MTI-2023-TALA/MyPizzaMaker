namespace backend.Dto
{
    public class Cart
    {
        public int CartId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public List<Pizza> Pizzas { get; set; }

        public List<string> StatusList { get; set; } = new List<string>
        {
            "En cours de création",
            "En attente de confirmation",
            "En cours de préparation",
            "A récupérer",
            "Servie",
            "Annulée"
        };
    }
}
