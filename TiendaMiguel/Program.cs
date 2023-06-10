using Microsoft.EntityFrameworkCore;
using TiendaMiguel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TallerCRUDContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TallerCRUDContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// ...

// ...

app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Verificar si el usuario no está autenticado
    if (!context.User.Identity.IsAuthenticated)
    {
        // Verificar si la solicitud no está en la página de Login o Registro
        if (context.Request.Path != "/Identity/Account/Login" && context.Request.Path != "/Identity/Account/Register")
        {
            // Redirigir a la página de Login
            context.Response.Redirect("/Identity/Account/Login");
            return;
        }
    }

    // Si el usuario está autenticado o la solicitud está en la página de Login o Registro,
    // continuar con el siguiente middleware
    await next.Invoke();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
