using System;
using System.Collections.Generic;
using System.Text;
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
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SkillJob> SkillJob { get; set; }
        public DbSet<ApplyDetails> ApplyDetails { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<EventJob> EventJob { get; set; }
        public DbSet<Training> Training { get; set; }
    }
}
