using System.ComponentModel.DataAnnotations;

namespace Flone.Data.Models
{
    public class BaseEntity
    {
            public DateTime? DateCreated { get; set; }

            [MaxLength(50)]
            public string? UserCreated { get; set; }

            public DateTime? DateModified { get; set; }

            [MaxLength(50)]
            public string? UserModified { get; set; }

            public bool IsDeleted { get; set; }
    }
}
