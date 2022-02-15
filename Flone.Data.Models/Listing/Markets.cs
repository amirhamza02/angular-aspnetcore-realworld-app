using System.ComponentModel.DataAnnotations;

namespace Flone.Data.Models.Listing
{
    public class Markets : BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }
        public bool IsDeleted {get;set;}
    }
}
