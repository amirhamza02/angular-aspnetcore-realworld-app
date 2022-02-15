namespace Flone.Api.Models.Users
{
    public class UpdateUserModel
    {
        public UpdateUserModel()
        {
            Roles = new string[0];
        }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string[] Roles { get; set; }
    }
}
