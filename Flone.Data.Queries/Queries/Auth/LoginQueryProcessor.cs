using Flone.Api.Common.Exceptions;
using Flone.Api.Models.Login;
using Flone.Api.Models.Users;
using Flone.Data.Models;
using Flone.Data.Queries.Models;
using Flone.Data.Repository.DAL;
using Flone.Data.Repository.Helpers;
using Flone.Security;
using Flone.Security.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flone.Data.Queries.Queries.Auth
{
    public class LoginQueryProcessor : ILoginQueryProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IUsersQueryProcessor _usersQueryProcessor;
        private readonly ISecurityContext _context;
        private readonly ILogger<LoginQueryProcessor> _logger;
        private Random _random;

        public LoginQueryProcessor(IUnitOfWork uow, ITokenBuilder tokenBuilder,
            IUsersQueryProcessor usersQueryProcessor, ISecurityContext context,
             ILogger<LoginQueryProcessor> logger)
        {
            _random = new Random();
            _uow = uow;
            _tokenBuilder = tokenBuilder;
            _usersQueryProcessor = usersQueryProcessor;
            _logger = logger;
            _context = context;
        }
        public UserWithToken Authenticate(string username, string password)
        {
           var user = (from u in  _uow.Query<User>()
                where u.Username == username && !u.IsDeleted 
                select u)
                .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                .FirstOrDefault();

            if (user == null)
            {
                _logger.LogError("username/password aren't right");
                throw new BadRequestException("username/password can't be blank");
            }

            if(string.IsNullOrEmpty(password) || !user.Password.VerifyWithBCrypt(password))
            {
                _logger.LogInformation("username/password aren't right");
                throw new BadRequestException("username/password aren't right");
            }

            var expiresIn = DateTime.Now + TokenAuthOption.ExpiresSpan;
            var token = _tokenBuilder.Build(user.Username, user.Roles.Select(x => x.Role.Name).ToArray(), expiresIn);

            return new UserWithToken
            {
                User = user,
                Token = token,
                ExpiresAt = expiresIn
            };
        }

        public async Task ChangePassword(ChangeUserPasswordModel requestModel)
        {
            await _usersQueryProcessor.ChangePassword(_context.User.Id, requestModel);
        }

        public async Task<User> Register(RegisterModel model)
        {
            var requestModel = new CreateUserModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                UserName = model.UserName
            };

            var user = await _usersQueryProcessor.Create(requestModel);
            return user;
        }
    }
}
