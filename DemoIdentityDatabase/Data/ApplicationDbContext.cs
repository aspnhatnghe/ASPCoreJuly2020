using System;
using System.Collections.Generic;
using System.Text;
using DemoIdentityDatabase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoIdentityDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Loai> Loais { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
