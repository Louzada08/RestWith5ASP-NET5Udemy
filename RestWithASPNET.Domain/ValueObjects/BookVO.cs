namespace RestWithASPNET.Domain.ValueObjects
{
    public class BookVO
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime Launch_Date { get; set; }
        public decimal Price { get; set; }
        public string title { get; set; }
    }
}
