using Flone.Api.Models.Login;
using Flone.Api.Models.Users;
using Flone.Data.Models;
using Flone.Data.Queries.Models;

namespace Flone.Data.Queries.Queries.Auth
{
    public interface ILoginQueryProcessor
    {
        UserWithToken Authenticate(string username, string password);
        Task<User> Register(RegisterModel model);
        Task ChangePassword(ChangeUserPasswordModel requestModel);
    }
}
