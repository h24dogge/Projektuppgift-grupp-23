// Data/IEmployeeRepository.cs
using Personalregister.Models;

namespace Personalregister.Data
{
    // "Interface Segregation Principle" (I i SOLID)
    // "Dependency Inversion Principle" (D i SOLID)
    // Vi definierar ett kontrakt för vad ett "register" måste kunna göra.
    // Detta låter oss byta databas (från SQLite till RAM t.ex.) utan att ändra Program.cs
    public interface IEmployeeRepository
    {
        Employee? GetEmployeeById(int id);
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> SearchEmployees(string searchTerm);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
        DateTime GetLastReadTime(); // Uppfyller kravet
    }
}