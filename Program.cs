using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GroepSpace2024.Data;
using Microsoft.AspNetCore.Identity;
using GroepSpace2024.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GroepSpace2024Context");
builder.Services.AddDbContext<MyDatabaseContext>(options => options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'GroepSpace2024Context' not found.")));
//builder.Services.AddDbContext<MyDatabaseContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("GroepSpace2024Context") ?? throw new InvalidOperationException("Connection string 'GroepSpace2024Context' not found.")));

builder.Services.AddDefaultIdentity<GroepSpace2024User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    MyDatabaseContext context = new MyDatabaseContext(services.GetRequiredService<DbContextOptions<MyDatabaseContext>>());
    MyDatabaseContext.DataInitializer(context);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
