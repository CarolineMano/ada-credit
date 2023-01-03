using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class UpdateClient
    {
        public static ClientService _clientService = new ClientService();

        public static void Show()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("***Atualizar cadastro de Cliente***");

                Console.Write("Digite o CPF do cliente a ser atualizado (apenas números): ");
                var document = Console.ReadLine();

                if (_clientService.GetClientByDocument(document).Active == false)
                    throw new Exception("O cliente informado está inativo.");

                Console.WriteLine($"{Environment.NewLine}*DADOS DO CLIENTE*");
                Console.WriteLine(_clientService.GetClientByDocument(document));

                Console.WriteLine($"{Environment.NewLine}Digite o novo e-mail desejado: ");
                var email = Console.ReadLine();

                _clientService.UpdateClient(document, email);

                Console.WriteLine($"Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}