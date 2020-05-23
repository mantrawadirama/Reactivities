﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reactivities.Domain;
namespace Reactivities.Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Activity> Activities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Value>().HasData(
                new Value { Id = 1, Name = "value 101" },
                new Value { Id = 2, Name = "value 102" },
                new Value { Id = 3, Name = "value 103" },
                new Value { Id = 4, Name = "value 104" }
            );
        }
    }
}