namespace RestWithASPNET.Domain.ValueObjects
{
    public class BookVO
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public DateTime Launch_Date { get; set; }
        public decimal Price { get; set; }
        public string title { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? updatedat { get; set; }
        public DateTime? deletedat { get; set; }
    }
}
