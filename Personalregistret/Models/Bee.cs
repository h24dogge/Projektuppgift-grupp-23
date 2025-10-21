// Models/Bee.cs
namespace Personalregister.Models
{
    // INHERITANCE: Bee *är* en Employee
    // Visar "Open-Closed Principle" (O i SOLID) - vi kan lägga till Bin
    // utan att ändra befintlig kod för Myror.
    public class Bee : Employee
    {
        public int WingCount { get; set; }

        // Tom konstruktor för EF Core
        private Bee() : base("Default Bee") { }

        public Bee(string name, int wingCount) : base(name)
        {
            WingCount = wingCount;
        }

        public override double CalculateTaxRate()
        {
            // Bin kanske har en annan skattemodell
            return 0.25;
        }

        public override string GetDetails()
        {
            string status = IsActive() ? "Aktiv" : "Inaktiv";
            return $"[BI] ID: {Id}, Namn: {Name}, Vingar: {WingCount}, Status: {status}, Skatt: {CalculateTaxRate() * 100}%";
        }
    }
}