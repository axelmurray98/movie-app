using App_movie_mvc.Data;
using App_movie_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        //incluir el dbcontext
        builder.Services.AddDbContext<MovieDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("MovieConnection"));
        });

        // Registrar Identity antes de Build
        builder.Services.AddIdentityCore<Usuario>(options =>
        {
            // No exigir confirmación de cuenta para permitir login inmediato durante desarrollo
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireUppercase = false;
        }
 )
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<MovieDbContext>()
     .AddSignInManager();

        builder.Services.AddAuthentication(opt =>
        {
            // Asegurar que la aplicación valide la cookie de Identity como esquema por defecto
            opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        })
            .AddIdentityCookies();

        builder.Services.ConfigureApplicationCookie(o =>
        {
            o.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            o.SlidingExpiration = true;
            o.LoginPath = "/Usuario/Login";
            o.AccessDeniedPath = "/Usuario/AccesDenied";
        });

        var app = builder.Build();
        //invocar la ejecucion del dbseeder con un using scope
        using (var scope = app.Services.CreateScope())
        {
            var service = scope.ServiceProvider;
            try
            {
                var context = service.GetRequiredService<MovieDbContext>();
                var userManager = service.GetRequiredService<UserManager<Usuario>>();
                var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                await DbSeeder.Seed(context, userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = service.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ocurrió un error al hacer el seeding");
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
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}