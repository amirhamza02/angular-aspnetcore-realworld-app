using AutoMapper;
using Flone.Api.Models.Users;
using Flone.Data.Queries.Models;

namespace Flone.Web.Maps.Auth
{
    public class UserWithTokenMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<UserWithToken, UserWithTokenModel>();
            map.ForMember(dest => dest.User, opt => opt.Ignore());

        }
    }
}
