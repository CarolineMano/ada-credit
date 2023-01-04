using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class AddNewClient
    {
        public static ClientService _clientService = new ClientService();

        public static void Show()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("***Cadastrar Novo Cliente***");

                Console.Write("Digite o nome do cliente: ");
                var name = Console.ReadLine();

                Console.Write("Digite o CPF do cliente (apenas números): ");
                var document = Console.ReadLine();

                Console.Write("Digite o email do cliente: ");
                var email = Console.ReadLine();

                _clientService.AddNewClient(name, document, email);

                Console.WriteLine($"Cliente {name} cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}