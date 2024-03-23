using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasOne(a => a.Driver)
                      .WithMany(d => d.Achievements)
                      .HasForeignKey(a => a.DriverId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Achievements_Driver");
            });
        }
    }
}
