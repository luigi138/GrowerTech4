using GrowerTech_MVC.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace _GrowerTech
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the caixa.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GrowerTechDbContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("OracleFIAP"));
            });

            //Ciclo de vida
            //builder.Services.AddScoped
            //builder.Services.AddSingleton
            //builder.Services.AddTransient *

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
        }
    }
}
