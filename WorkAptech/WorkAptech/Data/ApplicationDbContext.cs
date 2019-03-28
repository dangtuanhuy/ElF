using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorkAptech.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>(); base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SkillJob>()
                .HasKey(a => new { a.JobId, a.SkillId });
            

        }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
       
        public DbSet<ApplyDetails> ApplyDetails { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<EventJob> EventJob { get; set; }
        public DbSet<Training> Training { get; set; }

        public DbSet<Notification> Notification { get; set; }
        public DbSet<SkillJob> SkillJob { get; set; }
    }
}
