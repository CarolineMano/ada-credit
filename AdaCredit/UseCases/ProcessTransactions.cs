using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public class ProcessTransactions
    {
        public static TransactionService _transactionService = new TransactionService();

        public static void Show()
        {
            Console.Clear();

            Console.WriteLine("***Reconciliação Bancária***");

            _transactionService.ProcessTransactions();

            Console.WriteLine("Processamento completo!");

            Console.ReadKey();
        }
    }
}