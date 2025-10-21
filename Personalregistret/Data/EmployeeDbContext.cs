// Data/EmployeeDbContext.cs
using Microsoft.EntityFrameworkCore;
using Personalregister.Models;

namespace Personalregister.Data
{
    // EF Core DbContext för att hantera databasanslutningen och mappningen.
    // Detta är en implementation av datastrukturs-kravet.
    public class EmployeeDbContext : DbContext
    {
        private readonly string _databasePath;

        // DbSet är vår "Datastruktur" - en samling av Employee-objekt
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ant> Ants { get; set; }
        public DbSet<Bee> Bees { get; set; }

        public EmployeeDbContext(string databasePath)
        {
            _databasePath = databasePath;
            // Detta skapar databasen och tabellerna om de inte finns.
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Här talar vi om för EF Core att använda SQLite
            optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        }
    }
}