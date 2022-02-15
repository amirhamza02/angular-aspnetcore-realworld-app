using System.ComponentModel.DataAnnotations;

namespace Flone.Api.Models.Login
{
    public class LoginModel
    {
        [Required]
        public string ? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
