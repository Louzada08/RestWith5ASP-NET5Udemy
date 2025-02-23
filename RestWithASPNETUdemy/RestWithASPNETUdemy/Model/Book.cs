using RestWithASPNETUdemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("author")]
        public string Author { get; set; }

        [Column("launch_date")]
        public DateTime Launch_Date { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("title")]
        public string title { get; set; }
    }
}
