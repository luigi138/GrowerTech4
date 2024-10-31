using GrowerTech_MVC.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace _GrowerTech
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

        
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GrowerTechDbContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("OracleFIAP"));
            });

        

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
        }
    }
}
