using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Mapper;
using CsvHelper;
using CsvHelper.Configuration;

namespace AdaCredit.Persistence
{
    public class EmployeeRepository
    {
        private static List<Employee> _employees;
        private static CsvConfiguration _config;

        static EmployeeRepository()
        {
            _config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null
            };

            Initialize();
        }

        private static void Initialize()
        {
            try
            {
                using (var reader = new StreamReader(@$"Database\Employees.csv"))
                using (var csv = new CsvReader(reader, _config))
                {
                    _employees = csv.GetRecords<Employee>().ToList();
                }
            }
            catch (System.Exception)
            {
                var repositoryFolder = @$"\Database";
                bool folderExists = Directory.Exists(repositoryFolder);

                if (!folderExists)
                    Directory.CreateDirectory(repositoryFolder);

                _employees = new List<Employee>();
                _employees.Add(new Employee("Admin", "user"));

                using (var writer = new StreamWriter(@$"Database\Employees.csv"))
                using (var csv = new CsvWriter(writer, _config))
                {
                    csv.Context.RegisterClassMap<EmployeeMap>();
                    csv.WriteHeader<Employee>();
                }
            }
        }

        public Employee? GetEmployeeByUsername(string username)
        {
            return _employees.FirstOrDefault(e => e.Username == username);
        }

        public void AddNewEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public List<Employee> GetAllActiveEmployees()
        {
            return _employees.Where(e => e.Active == true).ToList();
        }

        public void Save()
        {
            using (var writer = new StreamWriter(@$"Database\Employees.csv"))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<EmployeeMap>();
                csv.WriteRecords<Employee>(_employees);
            }
        }
    }
}