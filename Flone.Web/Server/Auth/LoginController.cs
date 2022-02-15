using Flone.Api.Models.Login;
using Flone.Api.Models.Users;
using Flone.Data.Queries.Queries.Auth;
using Flone.Web.Maps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flone.Web.Server.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public LoginController(ILoginQueryProcessor loginQueryProcessor, IAutoMapper autoMapper)
        {
            _query = loginQueryProcessor;
            _mapper = autoMapper;
        }

        [HttpPost("Authenticate")]
        public UserWithTokenModel Authenticate([FromBody] LoginModel model)
        {
            var result = _query.Authenticate(model.UserName, model.Password);
            var resultModel = _mapper.Map<UserWithTokenModel>(result);

            return resultModel;
        }

        [HttpPost("Registration")]
        public async Task<UserModel> Register([FromBody] RegisterModel model)
        {
            var result = await _query.Register(model);
            var resultModel = _mapper.Map<UserModel>(result);
            return resultModel;
        }

        [HttpPut("Change-password")]
        [Authorize]
        public async Task ChangePassword([FromBody] ChangeUserPasswordModel requestModel)
        {
            await _query.ChangePassword(requestModel);
        }

    }
}
