using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace DAL.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<FileModel> Files { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileModel>()
                .HasOne(f => f.User)
                .WithMany(u => u.FileModels)
                .IsRequired();

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.FileModels)
            //    .WithOne(f => f.User)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
