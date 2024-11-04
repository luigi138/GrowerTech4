using GrowerTech_MVC.Services;
using GrowerTech_MVC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // 配置 Oracle 数据库连接
        var connectionString = Configuration.GetConnectionString("OracleFIAP");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseOracle(connectionString));

        // 配置 Google 身份验证
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(options =>
        {
            options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") 
                ?? throw new InvalidOperationException("GOOGLE_CLIENT_ID environment variable is not set");
            options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") 
                ?? throw new InvalidOperationException("GOOGLE_CLIENT_SECRET environment variable is not set");
            options.CallbackPath = "/signin-google";
        });

        // 注册服务
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<MLModelService>();

        // 启用 MVC
        services.AddControllersWithViews();

        // 配置会话
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // 配置日志
        services.AddLogging(logging =>
        {
            logging.AddConfiguration(Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                
            endpoints.MapControllerRoute(
                name: "agriculture",
                pattern: "agriculture/{action=Index}/{id?}",
                defaults: new { controller = "Agriculture" });
        });
    }
}
