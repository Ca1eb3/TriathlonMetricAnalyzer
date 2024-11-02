using TriathlonMetricAnalyzer.Models.StorageServices;

var builder = WebApplication.CreateBuilder(args);

// Set the application to listen on the port specified by the PORT environment variable
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");


// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the Storage Services as a singleton
builder.Services.AddSingleton<TokenStorageService>();
builder.Services.AddSingleton<AthleteStorageService>();
builder.Services.AddSingleton<SummaryActivitiesStorageService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
