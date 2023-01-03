using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class GetClientByAccountNumber
    {
        public static ClientService _clientService = new ClientService();

        public static void Show()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("***Consultar Cliente por Conta***");

                Console.Write("Digite o número da conta do cliente desejado (#####-#): ");
                var accountNumber = Console.ReadLine();

                var client = _clientService.GetClientByAccountNumber(accountNumber);
                
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