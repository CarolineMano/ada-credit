using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class GetClientByDocument
    {
        public static ClientService _clientService = new ClientService();

        public static void Show()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("***Consultar Cliente por CPF***");

                Console.Write("Digite o CPF do cliente desejado (apenas números): ");
                var document = Console.ReadLine();

                var client = _clientService.GetClientByDocument(document);

                Console.WriteLine(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadKey();
        }
    }
}