using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Persistence;
using AdaCredit.UseCases;

namespace AdaCredit.Services
{
    public class EmployeeService
    {
        private static EmployeeRepository _employeeRepository = new EmployeeRepository();

        public void AddNewEmployee(string name, string username)
        {
            if (!IsUsernameValid(username))
            {
                Console.WriteLine("O username não está disponível");
                Console.ReadKey();
                return;
            }

            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("O nome não pode ser vazio");
                Console.ReadKey();
                return;
            }

            if (String.IsNullOrEmpty(username))
            {
                Console.WriteLine("O username não pode ser vazio");
                Console.ReadKey();
                return;
            }

            var newEmployee = new Employee(name, username);

            _employeeRepository.AddNewEmployee(newEmployee);
            _employeeRepository.Save();

            Console.WriteLine("Funcionário cadastrado com sucesso");

            Console.ReadKey();
        }

        public void UpdatePassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                Console.WriteLine("Senha não pode ser nula");
                Console.ReadKey();
                return;
            }

            Login.LoggedInUser.UpdatePassword(password);
            _employeeRepository.Save();

            Console.WriteLine("Senha alterada com sucesso");

            Console.ReadKey();
        }

        private bool IsUsernameValid(string username)
        {
            var existingEmployee = _employeeRepository.GetEmployeeByUsername(username);

            if (existingEmployee != null)
                return false;

            return true;
        }
    }
}