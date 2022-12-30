using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class DeleteClient
    {
        public static ClientService _clientService = new ClientService();
        public static void Show()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("***Desativar Cliente***");

                Console.Write("Digite o CPF do cliente a ser desativado: ");
                var document = Console.ReadLine();

                _clientService.DeleteClient(document);
                
                Console.WriteLine("Cliente desativado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}