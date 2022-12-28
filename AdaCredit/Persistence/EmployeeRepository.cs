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

            using (var reader = new StreamReader("D:\\Meus Documentos\\Github\\code-at-cs-ada-credit\\AdaCredit\\Database\\Employees.csv"))
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
            // Save();
        }

        public void Save()
        {
            using (var writer = new StreamWriter("D:\\Meus Documentos\\Github\\code-at-cs-ada-credit\\AdaCredit\\Database\\Employees.csv"))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<EmployeeMap>();
                csv.WriteRecords<Employee>(_employees);
            }
        }
    }
}