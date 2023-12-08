using GroepSpace2024.Areas.Identity.Data;
using GroepSpace2024.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroepSpace2024.Data;

public class MyDatabaseContext : IdentityDbContext<GroepSpace2024User>
{
    public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<GroepSpace2024.Models.Groep> Groeps { get; set; } = default!;
    public DbSet<GroepSpace2024.Models.Message> Message { get; set; } = default!;

    public static async void DataInitializer(MyDatabaseContext context)//,UserManager userManager )
    {

        if(!context.Users.Any())
        {
            GroepSpace2024User dummyUser = new GroepSpace2024User
            {
                Id = "Dummy",
                Email = "dummy@mail.xx",
                UserName = "Dummy",
                FirstName = "Dummy",
                LastName = "Dummy",
                PasswordHash = "Dummy",
                LockoutEnabled = true,
                LockoutEnd = DateTime.MaxValue
            };
            context.Users.Add(dummyUser);
            context.SaveChanges();
            GroepSpace2024User adminUser = new GroepSpace2024User
            {
                Id = "Admin",
                Email = "admin@mail.xx",
                UserName = "Admin",
                FirstName = "Administrator",
                LastName = "GroepSpace"
            };
            //var result = userManager.CreateAsync(adminUser,"ABC_PW3333");
        }
        if (context.Groeps.Any())
        {
            foreach (Groep g in context.Groeps)
            {
                context.Groeps.Remove(g);
            }
            context.SaveChanges();
        }
        if (!context.Groeps.Any())
        {
            context.Groeps.Add(new Groep { Description = "Dummy", Name = "Dummy", Started = DateTime.Now, Ended = DateTime.MaxValue });
            context.SaveChanges();
        }
        Groep dummy = context.Groeps.FirstOrDefault(g => g.Name == "Dummy");
        if (!context.Message.Any())
        {
            context.Message.Add(new Models.Message { Title = "Dummy", Body = "", Sent = DateTime.Now, Deleted = DateTime.Now, Recipient = dummy });
            context.SaveChanges();
        }
    }


    public MyDatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

