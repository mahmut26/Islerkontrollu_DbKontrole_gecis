using DataLayer.Model_DBContext;
using DataLayer.Model_Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataLayer.Model_Servis
{
    public class TokenService //heh token burdan çıkıyor işte
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly Identity_DB _DB;

        public TokenService(UserManager<AppUser> userManager, IConfiguration configuration,Identity_DB dB)
        {
            _userManager = userManager;
            _configuration = configuration;
            _DB = dB;
        }

        public async Task<string> GenerateTokenAsync(AppUser user,int kadi)
        {
            var aaa = _DB.Users.SingleOrDefault(x => x.Id == user.Id); //şimdi burada kullanıcı tarafındaki id yi yollayıp hepsini alıyorum neden mi claim için
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, kadi.ToString()), //id int olarak gelmiş te eskiden kaldı galiba bu bakmak lazım aslında. Api iken sadece kolaydı çalışması tabi
            new Claim(JwtRegisteredClaimNames.Name, user.Email), //maili de token e yaz
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
            //new Claim("IsUser",aaa.IsKullanici).,
            //new Claim("IsYazar",aaa.IsYazar.ToString()),
            //new Claim("IsAdmin",aaa.IsAdmin.ToString()),
            new Claim("IsUser", aaa.IsKullanici ? "true" : "false"), //asıl claim i yaz
            new Claim("IsYazar", aaa.IsYazar ? "true" : "false"),//asıl claim i yaz
            new Claim("IsAdmin", aaa.IsAdmin ? "true" : "false")//asıl claim i yaz
        };

        

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //sha256 işte anlatmaya gerek yok görüyorsunuz

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
