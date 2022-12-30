using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class AddNewEmployee
    {
        public static EmployeeService _employeeService = new EmployeeService();

        public static void Show()
        {
            try
            {
                Console.Clear();

                Console.WriteLine("***Cadastrar Novo Funcionário***");

                Console.Write("Digite o nome do funcionário: ");
                var name = Console.ReadLine();

                Console.Write("Digite o username do funcionário: ");
                var username = Console.ReadLine();

                _employeeService.AddNewEmployee(name, username);

                Console.Write("Funcionário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}