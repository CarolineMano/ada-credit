using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class UpdateEmployeePassword
    {
        public static EmployeeService _employeeService = new EmployeeService();
        public static void Show()
        {
            try
            {
                Console.Clear();

                Console.WriteLine($"***Trocar a senha do usuário: {Login.LoggedInUser.Username}***");
                Console.WriteLine("Digite a nova senha desejada: ");

                var password = Console.ReadLine();

                _employeeService.UpdatePassword(password);

                Console.WriteLine("Senha alterada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}