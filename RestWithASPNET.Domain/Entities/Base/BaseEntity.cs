using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
