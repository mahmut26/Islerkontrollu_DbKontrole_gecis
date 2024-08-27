using DataLayer.Model_DBContext;
using DataLayer.Model_Login;
using DataLayer.Model_Servis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddControllersWithViews(); //ben mi yapm���m

            builder.Services.AddEndpointsApiExplorer();//ben mi yapm���m

            builder.Services.AddDbContext<Blog_DB>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connStr"));//db ayar�
            });

            builder.Services.AddDbContext<Identity_DB>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));//db ayar�
            });

            builder.Services.AddAuthentication(X => {
                X.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //bu da u�ra�t�rd� yalan yok jwt ayar� bu bak token
                X.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                     options.TokenValidationParameters = new TokenValidationParameters
                        {
                              ValidateIssuer = true,
                              ValidateAudience = true,
                              ValidateLifetime = true,
                              ValidateIssuerSigningKey = true,
                              ValidIssuer = builder.Configuration["Jwt:Issuer"],
                              ValidAudience = builder.Configuration["Jwt:Issuer"],
                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                });

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<Identity_DB>().AddDefaultTokenProviders(); //ben mi yapm���m

            builder.Services.AddScoped<TokenService>(); //servis eklemesi bunu ben yapmad�m yalan yok 

            builder.Services.AddScoped<EmailService>();//servis eklemesi bunu ben yapmad�m yalan yok 

            builder.Services.AddSingleton<EmailService>();//servis eklemesi bunu ben yapmad�m yalan yok burada bi�iler fazla diyordu birileri ama bunlars�z da �al��m�yordu sebebini bilmiyorum ama �al���yorsa dokunma kural� uyguland� !!




            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth = 1024; //Hata veriyordu ��z�m buymu� bilmiyordum
            });
            builder.Services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false; //Hata ��z�m�nden geldi herhalde bu
            });
            builder.Services.AddAuthorization(options => //policy eklemece bura
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim("IsAdmin", "true")); 

                options.AddPolicy("Yazar", policy =>
                    policy.RequireClaim("IsYazar","true")); 

                options.AddPolicy("Kullanici", policy =>
                    policy.RequireClaim("IsUser", "true")); 
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization(); //buras� ters olacak demeyin �al���yorsa dokunmam ben 
            app.UseAuthentication();
            


            app.MapControllers();

            app.Run();
        }
    }
}
