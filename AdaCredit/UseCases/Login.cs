using System;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class Login
    {
        private static LoginService _loginService = new LoginService();
        public static Employee? LoggedInUser { get; private set; }

        public static void Show()
        {
            var loggedIn = false;

            try
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine($"***Login***{Environment.NewLine}");

                    Console.Write("Digite o nome do usuário: ");
                    var username = Console.ReadLine();

                    Console.Write("Digite a senha do usuário: ");
                    var password = Console.ReadLine();

                    LoggedInUser = _loginService.ValidateCredentials(username, password);

                    loggedIn = LoggedInUser is default(Employee) ? false : true;

                } while (!loggedIn);

                Console.WriteLine("Usuário e senha válidos!");
                Console.ReadKey();

                if (LoggedInUser.FirstLogin == true)
                {
                    Console.Clear();

                    Console.WriteLine($"***Você precisará trocar a senha padrão do usuário: {LoggedInUser.Username}***");
                    Task.Delay(1500).Wait();

                    UpdateEmployeePassword.Show();

                    _loginService.UpdatePasswordFirstLogin(LoggedInUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Login.Show();
            }
        }
    }
}