
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/api/weather/{city}", (string city) =>
{
    // Sahte API: Örnek hava durumu verileri döndürüyor.
    var weatherData = new
    {
        City = city,
        Temp = 20.5,
        MinTemp = 15.0,
        MaxTemp = 25.0,
        Description = "Clear Sky"
    };

    return Results.Json(weatherData);
});

app.Run();
    