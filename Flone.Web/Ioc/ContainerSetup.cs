using Flone.Data.Queries.Queries.Auth;
using Flone.Data.Queries.Queries.Listing;
using Flone.Data.Repository.DAL;
using Flone.Security;
using Flone.Security.Auth;
using Flone.Web.Filter;
using Flone.Web.Helper;
using Flone.Web.Maps;
using Flone.Web.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Flone.Web.Ioc
{
    public static class ContainerSetup
    {
        internal static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureContext(services, configuration);
            ConfigureAuth(services);
            ConfigureInject(services);
            ConfigureAutoMapper(services);
            AddQueries(services);
        }

        private static void ConfigureContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Data:main"];
            var logDBConnection = configuration["Data:logDB"];


            var seriConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(seriConfiguration)
                .WriteTo.MongoDB(logDBConnection)
                .CreateLogger();


            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork>(ctx => new EFUnitOfWork(ctx.GetRequiredService<MainDbContext>()));

            services.AddScoped<IActionTransactionHelper, ActionTransactionHelper>();
            services.AddScoped<UnitOfWorkFilterAttribute>();
        }
        private static void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (o) =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = TokenAuthOption.Key,
                        ValidAudience = TokenAuthOption.Audience,
                        ValidIssuer = TokenAuthOption.Issuer,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
        private static void ConfigureInject(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<ISecurityContext, SecurityContext>();
        }
        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mapperConfig = AutoMapperConfigurator.Configure();
            var mapper = mapperConfig.CreateMapper();
            services.AddScoped(x => mapper);

            services.AddTransient<IAutoMapper, AutoMapperAdapter>();
            services.AddTransient<Microsoft.Extensions.Logging.ILogger>(svc => svc.GetRequiredService<ILogger<ActionTransactionHelper>>());
        }
        private static void AddQueries(IServiceCollection services)
        {
            var userQProcessor = typeof(IUsersQueryProcessor);
            var qUserProcesses = (from t in userQProcessor.GetTypeInfo().Assembly.GetTypes()
                                  where t.Namespace == userQProcessor.Namespace
                                        && t.GetTypeInfo().IsClass
                                        && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                                  select t).ToArray();

            foreach (var qUserProcess in qUserProcesses)
            {
                var interfaceQ = qUserProcess.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, qUserProcess);
            }

            services.AddScoped<ICategoryQueryProcessor, CategoryQueryProcessor>();
        }
    }
}
