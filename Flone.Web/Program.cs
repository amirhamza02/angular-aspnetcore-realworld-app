using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);



builder.Host.UseSerilog();


var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//InitDatabase(app);

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseSerilogRequestLogging();

app.UseRouting();

app.UseAuthorization();
startup.InitDatabase(app);

//builder.Services.AddRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
