using System.ComponentModel.DataAnnotations;

namespace Flone.Api.Models.Common
{
    public class BaseModel
    {
        public DateTime? DateCreated { get; set; }

        [MaxLength(50)]
        public string? UserCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [MaxLength(50)]
        public string? UserModified { get; set; }
    }
}
