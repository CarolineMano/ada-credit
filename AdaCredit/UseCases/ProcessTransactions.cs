using System;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public class ProcessTransactions
    {
        public static TransactionService _transactionService = new TransactionService();

        public static void Show()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("***Reconciliação Bancária***");

                _transactionService.ProcessTransactions();

                Console.WriteLine("Processamento completo!");
            }
            catch (Exception ex)
            {   
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}