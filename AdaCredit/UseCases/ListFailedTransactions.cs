using System;
using AdaCredit.Enum;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class ListFailedTransactions
    {
        private static TransactionService _transactionService = new TransactionService();
        public static void Show()
        {
            try
            {
                Console.Clear();

                Console.WriteLine($"***Transações com falha***{Environment.NewLine}");

                var transactions = _transactionService.GetFailedTransactions(TransactionFolder.Failed);

                foreach (var transaction in transactions)
                {
                    Console.WriteLine("********************");
                    Console.WriteLine(transaction);
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