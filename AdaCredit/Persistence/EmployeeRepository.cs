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

            using (var reader = new StreamReader("../AdaCredit/Database/Employees.csv"))
            using (var csv = new CsvReader(reader, _config))
            {
                _employees = csv.GetRecords<Employee>().ToList();
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
            // var activeEmployees = new List<Employee>();

            // foreach (var employee in _employees)
            // {
            //     activeEmployees.Add(employee);
            // }

            // return activeEmployees.AsReadOnly().ToList();

            return _employees.Where(e => e.Active == true).ToList();
        }

        public void Save()
        {
            using (var writer = new StreamWriter("../AdaCredit/Database/Employees.csv"))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<EmployeeMap>();
                csv.WriteRecords<Employee>(_employees);
            }
        }
    }
}