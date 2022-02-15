using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flone.Data.Models
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50), Required]
        public string? Name { get; set; }
    }
}
