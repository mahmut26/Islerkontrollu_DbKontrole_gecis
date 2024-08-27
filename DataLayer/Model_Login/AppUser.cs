using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_Login
{
    public class AppUser : IdentityUser
    {
        public bool IsAdmin { get; set; } //claim olacak abisi büyüyünce
        public bool IsYazar { get; set; } //claim olacak abisi büyüyünce
        public bool IsKullanici { get; set; } //claim olacak abisi büyüyünce

    }
}
