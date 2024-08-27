using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using DataLayer.Model_DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Abstractions;

namespace Odev_v9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddHttpClient();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => //login logout için yapýldý bu
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
            builder.Services.AddDbContext<Blog_DB>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connStr")); //bi yerde kullandým diye yaptým bunu
            });

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

            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
