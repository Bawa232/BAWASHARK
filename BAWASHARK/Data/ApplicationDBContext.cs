using Microsoft.EntityFrameworkCore;
using BAWASHARK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BAWASHARK.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
