using System;
using AdaCredit.Entities;
using AdaCredit.Enum;
using AdaCredit.Services;
using BetterConsoleTables;

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

                ColumnHeader[] headers = new[]
                {
                    new ColumnHeader("Banco de Origem"),
                    new ColumnHeader("Agência de Origem"),
                    new ColumnHeader("Conta de Origem"),
                    new ColumnHeader("Banco de Destino"),
                    new ColumnHeader("Agência de Destino"),
                    new ColumnHeader("Conta de Destino"),
                    new ColumnHeader("Tipo de Transação"),
                    new ColumnHeader("Sentido da Transação"),
                    new ColumnHeader("Valor da Transação"),
                    new ColumnHeader("Erro"),
                };

                var transactions = _transactionService.GetFailedTransactions(TransactionFolder.Failed);

                Table table = new Table(TableConfiguration.Unicode(), headers);

                foreach (var transaction in transactions)
                {
                    // table.AddRow(transaction.OriginBankId, 
                    //             transaction.OriginBankBranch,
                    //             transaction.OriginBankAccountNumber,
                    //             transaction.RecipientBankId,
                    //             transaction.RecipientBankBranch,
                    //             transaction.RecipientBankAccountNumber,
                    //             transaction.TransactionType,
                    //             transaction.TransactionFlow,
                    //             transaction.Value.ToString("C"),
                    //             transaction.ErrorDetail);
                    Console.WriteLine("********************");
                    Console.WriteLine(transaction);
                }

                // Console.Write(table.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}