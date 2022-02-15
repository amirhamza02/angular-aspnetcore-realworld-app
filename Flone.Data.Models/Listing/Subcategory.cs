using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flone.Data.Models.Listing
{
    public class Subcategory
    {
        public int Id { get; set; }

        [Column(TypeName="varchar"),Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
