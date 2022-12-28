using System;
using AdaCredit.Entities;
using AdaCredit.Persistence;

namespace AdaCredit.UseCases
{
    public static class Login
    {
        public static EmployeeRepository _employeeRepository = new EmployeeRepository();
        public static void Show()
        {
            var loggedIn = false;

            do
            {
                Console.Clear();

                Console.Write("Digite o nome do usuário: ");
                var username = Console.ReadLine();
                
                Console.Write("Digite a senha do usuário: ");
                var password = Console.ReadLine();

                var employee = _employeeRepository.GetEmployeeByUsername(username);

                if (employee == null)
                {
                    Console.WriteLine("Usuário informado não existe.");
                    Console.ReadKey();
                    continue;
                }

                if (!ValidatePassword(employee, password))
                {
                    Console.WriteLine("Senha inválida.");
                    Console.ReadKey();
                    continue;
                } 

                Console.WriteLine("Usuário e senha válidos!");
                loggedIn = true;

            } while (!loggedIn);

            Console.ReadKey();
        }

        private static bool ValidatePassword(Employee employee, string password)
        {
            var hashedPassword = BC.HashPassword(password, employee.Salt);
            
            return hashedPassword == employee.PasswordHash;
        }
    }
}