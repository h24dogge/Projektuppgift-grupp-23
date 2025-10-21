// Data/EmployeeRepository.cs
using Microsoft.EntityFrameworkCore;
using Personalregister.Models;

namespace Personalregister.Data
{
    // Detta är vår SQLite-implementation av IEmployeeRepository.
    // All databaslogik är INKAPSLAD (Encapsulation från APIE) här.
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        private readonly DateTime _lastReadTime; //

        public EmployeeRepository(string databasePath)
        {
            _context = new EmployeeDbContext(databasePath);
            _lastReadTime = DateTime.Now; //
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var emp = GetEmployeeById(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            // Vi returnerar både Myror och Bin (Polymorphism)
            return _context.Employees.ToList().Select(SetReadTime);
        }

        public Employee? GetEmployeeById(int id)
        {
            var emp = _context.Employees.Find(id);
            return (emp != null) ? SetReadTime(emp) : null;
        }

        public DateTime GetLastReadTime()
        {
            return _lastReadTime;
        }

        public IEnumerable<Employee> SearchEmployees(string searchTerm)
        {
            if (int.TryParse(searchTerm, out int id))
            {
                return _context.Employees
                   .Where(e => e.Id == id)
                   .ToList()
                   .Select(SetReadTime);
            }

            return _context.Employees
                .Where(e => e.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToList()
                .Select(SetReadTime);
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        private Employee SetReadTime(Employee emp)
        {
            // Uppfyller kravet
            emp.LastReadTime = _lastReadTime;
            return emp;
        }
    }
}