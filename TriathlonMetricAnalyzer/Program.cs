
var builder = WebApplication.CreateBuilder(args);

// Set the application to listen on the port specified by the PORT environment variable
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// Set environment variables
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    // Add environment variables to configuration
    builder.Configuration["Authentication:Client_Id"] = Environment.GetEnvironmentVariable("Client_Id");
    builder.Configuration["Authentication:Client_Secret"] = Environment.GetEnvironmentVariable("Client_Secret");
    builder.Configuration["Authentication:Verified_Token"] = Environment.GetEnvironmentVariable("Verified_Token");
}
else
{
    // Load configuration from appsettings.json for all environments
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
}


// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
