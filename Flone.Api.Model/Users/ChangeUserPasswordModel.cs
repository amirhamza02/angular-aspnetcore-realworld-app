using System.ComponentModel.DataAnnotations;

namespace Flone.Api.Models.Users
{
    public class ChangeUserPasswordModel
    {
        public ChangeUserPasswordModel(string password)
        {
            Password = password;
        }
        [Required]
        public string Password { get; set; }
    }
}
