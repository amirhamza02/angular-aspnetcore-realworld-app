using System.ComponentModel.DataAnnotations;

namespace Flone.Data.Models.Listing
{
    public class Category : BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
       
        [MaxLength(50)]
        public string? CatgoryName { get; set; }
        public int MarketId  { get; set; }
        public virtual Markets? Markets { get; set; }
    }
}
