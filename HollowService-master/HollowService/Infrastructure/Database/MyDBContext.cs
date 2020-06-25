using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HollowService.Model
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options): base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
              
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
