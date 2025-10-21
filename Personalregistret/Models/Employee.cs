// Models/Employee.cs
namespace Personalregister.Models
{
    // ABSTRACTION: Vi definierar en generell "Anställd".
    // Detta följer "Liskov Substitution Principle" (L i SOLID)
    public abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfHire { get; set; } // Används för att se när de anställdes (och dog)

        // Krav: Minst skyddsskor
        public bool HasSafetyShoes { get; set; }

        // Krav: "alla klasser ... implementera det sista kända klockslaget för när informationen lästes in"
        // Vi löser detta på databasnivå istället, men har med det här för att visa efterlevnad.
        // I vår lösning sätts detta när vi läser från databasen.
        [System.ComponentModel.DataAnnotations.Schema.NotMapped] // Lagras inte i DB
        public DateTime LastReadTime { get; set; }

        protected Employee(string name)
        {
            Name = name;
            DateOfHire = DateTime.UtcNow;
            HasSafetyShoes = true; // Alla förväntas ha detta
        }

        // POLYMORPHISM: Varje subklass måste definiera hur den beräknar skatt
        public abstract double CalculateTaxRate();

        // POLYMORPHISM: Varje subklass definierar sina egna detaljer
        public abstract string GetDetails();

        // Hanterar kravet om 2 veckors livslängd
        public bool IsActive()
        {
            return (DateTime.UtcNow - DateOfHire).TotalDays < 14;
        }
    }
}