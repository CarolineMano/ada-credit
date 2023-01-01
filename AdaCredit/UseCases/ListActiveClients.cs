using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class ListActiveClients
    {
        public static ClientService _clientService = new ClientService();
    
        public static void Show()
        {
            try
            {
                Console.Clear();

                Console.WriteLine($"***Clientes ativos***{Environment.NewLine}");

                var clients = _clientService.GetAllActiveClients();

                foreach (var client in clients)
                {
                    Console.WriteLine("********************");
                    Console.WriteLine(client);
                }  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}