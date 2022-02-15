using Flone.Data.Repository.DAL;
using Flone.Web.Ioc;
using Microsoft.EntityFrameworkCore;

internal class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }



    public void ConfigureServices(IServiceCollection services)
    {
       // services.AddApplicationInsightsTelemetry(Configuration);
        ContainerSetup.Setup(services, Configuration);
    }



    internal void InitDatabase(WebApplication application)
    {
        IApplicationBuilder app = application;
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<MainDbContext>();
            if (context != null)
            {
                context.Database.Migrate();
            }
        }
    }
}