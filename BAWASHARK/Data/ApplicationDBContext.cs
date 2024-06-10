﻿using Microsoft.EntityFrameworkCore;
using BAWASHARK.Models;

namespace BAWASHARK.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}