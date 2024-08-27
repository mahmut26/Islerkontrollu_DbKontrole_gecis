using DataLayer.Model_Login;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_DBContext
{
    public class Identity_DB : IdentityDbContext<AppUser>
    {
        public Identity_DB(DbContextOptions<Identity_DB> options) : base(options)
        {

        }
    }
}
