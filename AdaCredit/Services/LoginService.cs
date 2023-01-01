using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Persistence;

namespace AdaCredit.Services
{
    public class LoginService
    {
        private static EmployeeRepository _employeeRepository = new EmployeeRepository();
        private static EmployeeService _employeeService = new EmployeeService();

        public Employee? ValidateCredentials(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                Console.WriteLine("Usuário e senha não podem ser vazios.");
                Console.ReadKey();
                return default(Employee);
            }

            var loggedInUser = _employeeRepository.GetEmployeeByUsername(username);

            if (loggedInUser == null)
            {
                Console.WriteLine("Usuário informado não existe.");
                Console.ReadKey();
                return default(Employee);
            }

            if (!ValidatePassword(loggedInUser, password))
            {
                Console.WriteLine("Senha inválida.");
                Console.ReadKey();
                return default(Employee);
            }

            loggedInUser.UpdateLastLoggedIn();

            _employeeRepository.Save();

            return loggedInUser;
        }

        public void UpdatePasswordFirstLogin(Employee loggedInUser)
        {
            loggedInUser.UpdateFirstLogin();
            _employeeRepository.Save();
        }

        private bool ValidatePassword(Employee employee, string password)
        {
            var hashedPassword = BC.HashPassword(password, employee.Salt);

            return hashedPassword == employee.PasswordHash;
        }
    }
}