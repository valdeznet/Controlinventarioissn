using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(o => 
{
    _ = o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); //aca ya queda armada la base de datos

});

builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true; //mail unico
    cfg.Password.RequireDigit = false; 
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<DataContext>();


builder.Services.AddTransient<SeedDb>();  //inyeccion addtransient es que la voy a usar una sola vez
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();
builder.Services.AddScoped<IUserHelper, UserHelper>();

var app = builder.Build();
SeedData();

void SeedData()
{
    {
        IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();//todo para hacer inyeccion a mano

        using (IServiceScope? scope = scopedFactory.CreateScope())
        {
            SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
            service.SeedAsync().Wait();
        }
    }

}

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
app.UseAuthentication(); //mi aplicacion va usar autenticacion de usuario y password

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
