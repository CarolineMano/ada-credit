using System;
using System.Collections.Generic;
using AdaCredit.Entities;
using AdaCredit.Enum;
using AdaCredit.Persistence;

namespace AdaCredit.Services
{
    // TODO Incluir detalhes de erro da transação que falha
    public class TransactionService
    {
        private static TransactionRepository _transactionRepository = new TransactionRepository();
        private static ClientRepository _clientRepository = new ClientRepository();
        private Stack<string> _fileNames = new Stack<string>();

        public void ProcessTransactions()
        {
            _fileNames = _transactionRepository.GetFileNames();
            
            do
            {
                ProcessTransactionFile(_fileNames.Peek());
                _fileNames.Pop();
            } while (_fileNames.Count != 0);
        }

        private void ProcessTransactionFile(string fileName)
        {
            var transactionsPending = new List<Transaction>();
            var transactionsCompleted = new List<Transaction>();
            var transactionsFailed = new List<Transaction>();
            Client? clientOrigin;
            Client? clientRecipient;

            transactionsPending = _transactionRepository.GetTransactionsFromFile(fileName);

            foreach (var transaction in transactionsPending)
            {
                clientOrigin = GetOriginClient(transaction);
                clientRecipient = GetRecipientClient(transaction);

                try
                {
                    if (transaction.TransactionType == TransactionType.TEF)
                        ProcessTef(transaction, clientOrigin, clientRecipient);
                    else if (transaction.TransactionType == TransactionType.DOC)
                        Console.WriteLine("Implementar DOC");// TODO Implementar DOC
                    else if (transaction.TransactionType == TransactionType.TED)
                        Console.WriteLine("Implementar TED");// TODO Implementar TED

                    transactionsCompleted.Add(transaction);
                }
                catch (System.Exception)
                {                    
                    transactionsFailed.Add(transaction);
                }                
            }

            _transactionRepository.SaveCompleted(transactionsCompleted, fileName);
            _transactionRepository.SaveFailed(transactionsFailed, fileName);
        }

        private void ProcessTef(Transaction transaction, Client originClient, Client recipientClient)
        {
            var valid = false;

            if (transaction.OriginBankId != transaction.RecipientBankId)
                throw new Exception("Bancos diferentes, não é possível realizar TEF");

            if (transaction.TransactionFlow == TransactionFlow.Payment)
            {
                UpdateAccountsPayment(transaction.Value, originClient, recipientClient);
            }

            if (transaction.TransactionFlow == TransactionFlow.Deposit)
            {
                UpdateAccountsDeposit(transaction.Value, originClient, recipientClient);
            }
        }

        private Client? GetOriginClient(Transaction transaction)
        {
            if (transaction.OriginBankId != "777")
                return default(Client);

            var accountNumberFormatted = transaction.OriginBankAccountNumber.Insert(5, "-");

            var clientFromDb = _clientRepository.GetByAccountNumber(accountNumberFormatted);

            if (clientFromDb is default(Client))
                throw new Exception("Cliente informado não existe");

            return clientFromDb;
        }

        private Client? GetRecipientClient(Transaction transaction)
        {
            if (transaction.RecipientBankId != "777")
                return default(Client);

            var accountNumberFormatted = transaction.OriginBankAccountNumber.Insert(5, "-");

            var clientFromDb = _clientRepository.GetByAccountNumber(accountNumberFormatted);

            if (clientFromDb is default(Client))
                throw new Exception("Cliente informado não existe");

            return clientFromDb;
        }

        private void UpdateAccountsPayment(decimal value, Client originClient, Client recipientClient)
        {
            var valid = false;

            valid = originClient.UpdateBalance(value, TransactionFlow.Payment);

            if (!valid)
                throw new Exception("Conta de origem não tem saldo suficiente para a transação");

            recipientClient.UpdateBalance(value, TransactionFlow.Deposit);

            _clientRepository.Save();
        }

        private void UpdateAccountsDeposit(decimal value, Client originClient, Client recipientClient)
        {
            var valid = false;

            valid = recipientClient.UpdateBalance(value, TransactionFlow.Payment);

            if (!valid)
                throw new Exception("Conta de destino não tem saldo suficiente para a transação");

            originClient.UpdateBalance(value, TransactionFlow.Deposit);

            _clientRepository.Save();
        }
    }
}