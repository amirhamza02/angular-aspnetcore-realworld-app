using System.ComponentModel.DataAnnotations;

namespace Flone.Data.Models
{
    public class UserRole
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        [Required]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
