using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDMToM.Models;
using Microsoft.EntityFrameworkCore;
namespace CRUDMToM.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {  }
        public DbSet<Employee> Empployees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Employeeskill> EmployeeSkills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Employeeskill>().HasKey(ES => new { ES.EmployeeId, ES.SkillId });
            modelBuilder.Entity<Skill>().HasData(
            new Skill {Id=1, SkillName = "Communication" },
            new Skill {Id=2,  SkillName = "Decision-Making" },
            new Skill { Id = 3, SkillName = "Flexibility" },
            new Skill { Id = 4, SkillName = "Innovation" },
            new Skill { Id = 5, SkillName = "Integrity" },
            new Skill { Id = 6, SkillName = "Leadership" },
            new Skill { Id = 7, SkillName = "Time Management" },
            new Skill { Id = 8, SkillName = "Negotiation" }
            );
        }
    }

}
