using System;
using AdaCredit.Entities;
using AdaCredit.Persistence;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class Login
    {
        private static LoginService _loginService = new LoginService();
        private static EmployeeService _employeeService = new EmployeeService();
        public static Employee? LoggedInUser { get; private set; }
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

                LoggedInUser = _loginService.ValidateCredentials(username, password);

                loggedIn = LoggedInUser is default(Employee) ? false : true;

            } while (!loggedIn);

            Console.ReadKey();

            if (LoggedInUser.FirstLogin == true)
            {
                Console.Clear();

                Console.WriteLine($"***Você deve trocar a senha padrão do usuário: {LoggedInUser.Username}***");
                Console.WriteLine("Digite a nova senha desejada: ");

                var password = Console.ReadLine();

                _loginService.UpdatePasswordFirstLogin(LoggedInUser, password);
            }
        }
    }
}