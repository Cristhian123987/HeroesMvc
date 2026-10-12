using System.Text.Encodings.Web;
using HeroesMvc.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("HeroesClase1")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión HeroesClase1.");

builder.Services.AddControllersWithViews()
    .AddJsonOptions(o =>
    {
        // JSON legible en el navegador (indentado y con tildes sin escapar)
        o.JsonSerializerOptions.WriteIndented = true;
        o.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

builder.Services.AddDbContext<HeroesDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

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

app.Run();
