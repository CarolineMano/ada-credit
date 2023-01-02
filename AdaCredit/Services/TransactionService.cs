using System;
using System.Collections.Generic;
using System.Globalization;
using AdaCredit.Entities;
using AdaCredit.Enum;
using AdaCredit.Persistence;

namespace AdaCredit.Services
{
    public class TransactionService
    {
        private static TransactionRepository _transactionRepository = new TransactionRepository();
        private static ClientRepository _clientRepository = new ClientRepository();
        private Stack<string> _fileNames = new Stack<string>();

        public List<TransactionFailed> GetFailedTransactions(TransactionFolder transactionFolder)
        {
            _fileNames = _transactionRepository.GetFileNames(transactionFolder);
            var transactionsFromFolder = new List<TransactionFailed>();

            do
            {
                transactionsFromFolder.AddRange((IEnumerable<TransactionFailed>)_transactionRepository.GetFailedTransactionsFromFile(transactionFolder, _fileNames.Peek()));
                _fileNames.Pop();
            } while (_fileNames.Count != 0);

            return transactionsFromFolder;
        } 

        public void ProcessTransactions()
        {
            _fileNames = _transactionRepository.GetFileNames(TransactionFolder.Pending);

            do
            {
                ProcessTransactionFile(TransactionFolder.Pending, _fileNames.Peek());
                _fileNames.Pop();
            } while (_fileNames.Count != 0);
        }

        private void ProcessTransactionFile(TransactionFolder transactionFolder, string fileName)
        {
            var transactionsPending = new List<Transaction>();
            var transactionsCompleted = new List<Transaction>();
            var transactionsFailed = new List<TransactionFailed>();
            Client? clientOrigin;
            Client? clientRecipient;
            DateTime transactionDate;

            transactionsPending = _transactionRepository.GetTransactionsFromFile(transactionFolder, fileName);

            foreach (var transaction in transactionsPending)
            {
                clientOrigin = GetOriginClient(transaction);
                clientRecipient = GetRecipientClient(transaction);
                transactionDate = GetTransactionDate(fileName);

                try
                {
                    if (clientOrigin is default(Client) && clientRecipient is default(Client))
                        throw new Exception("Nenhum cliente da transação é da AdaCredit");
                    else if (transaction.TransactionType == TransactionType.TEF)
                        ProcessTef(transaction, clientOrigin, clientRecipient);
                    else if (transaction.TransactionType == TransactionType.DOC)
                        ProcessDoc(transaction, clientOrigin, clientRecipient, transactionDate);
                    else if (transaction.TransactionType == TransactionType.TED)
                        ProcessTed(transaction, clientOrigin, clientRecipient, transactionDate);

                    transactionsCompleted.Add(transaction);
                }
                catch (Exception ex)
                {
                    transactionsFailed.Add(new TransactionFailed(transaction.OriginBankId, transaction.OriginBankBranch, transaction.OriginBankAccountNumber, transaction.RecipientBankId, transaction.RecipientBankBranch, transaction.RecipientBankAccountNumber, transaction.TransactionType, transaction.TransactionFlow, transaction.Value, ex.Message));
                }
            }

            _transactionRepository.SaveCompleted(transactionsCompleted, fileName);
            _transactionRepository.SaveFailed(transactionsFailed, fileName);
        }

        private void ProcessTef(Transaction transaction, Client originClient, Client recipientClient)
        {
            var fare = 0M;

            if (transaction.OriginBankId != transaction.RecipientBankId)
                throw new Exception("Bancos diferentes, não é possível realizar TEF");

            if (transaction.TransactionFlow == TransactionFlow.Payment)
            {
                UpdateAccountsPayment(transaction.Value, fare, originClient, recipientClient);
            }

            if (transaction.TransactionFlow == TransactionFlow.Deposit)
            {
                UpdateAccountsDeposit(transaction.Value, fare, originClient, recipientClient);
            }
        }

        private void ProcessTed(Transaction transaction, Client originClient, Client recipientClient, DateTime transactionDate)
        {
            var fare = 5M;

            if (transactionDate.CompareTo(new DateTime(2022, 12, 01)) < 0)
                fare = 0;

            if (transaction.TransactionFlow == TransactionFlow.Payment)
            {
                UpdateAccountsPayment(transaction.Value, fare, originClient, recipientClient);
            }

            if (transaction.TransactionFlow == TransactionFlow.Deposit)
            {
                UpdateAccountsDeposit(transaction.Value, fare, originClient, recipientClient);
            }
        }

        private void ProcessDoc(Transaction transaction, Client originClient, Client recipientClient, DateTime transactionDate)
        {
            var fare = 0M;

            if (transactionDate.CompareTo(new DateTime(2022, 11, 30)) > 0)
            {
                fare = transaction.Value * 0.01M < 5m ? transaction.Value * 0.01M : 5M;
                fare += 1M;
            }

            if (transaction.TransactionFlow == TransactionFlow.Payment)
            {
                UpdateAccountsPayment(transaction.Value, fare, originClient, recipientClient);
            }

            if (transaction.TransactionFlow == TransactionFlow.Deposit)
            {
                UpdateAccountsDeposit(transaction.Value, fare, originClient, recipientClient);
            }
        }

        private DateTime GetTransactionDate(string fileName)
        {
            var dateArray = fileName.Split('-');
            var date = dateArray[dateArray.Length - 1].Substring(0, 8);

            return DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
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

            var accountNumberFormatted = transaction.RecipientBankAccountNumber.Insert(5, "-");

            var clientFromDb = _clientRepository.GetByAccountNumber(accountNumberFormatted);

            if (clientFromDb is default(Client))
                throw new Exception("Cliente informado não existe");

            return clientFromDb;
        }

        private void UpdateAccountsPayment(decimal value, decimal fare, Client originClient, Client recipientClient)
        {
            var valid = true;

            if (!(originClient is default(Client)))
                valid = originClient.UpdateBalance(value + fare, TransactionFlow.Payment);

            if (!valid)
                throw new Exception("Conta de origem não tem saldo suficiente para a transação");

            if (!(recipientClient is default(Client)))
                recipientClient.UpdateBalance(value, TransactionFlow.Deposit);

            _clientRepository.Save();
        }

        private void UpdateAccountsDeposit(decimal value, decimal fare, Client originClient, Client recipientClient)
        {
            var valid = true;

            if (!(recipientClient is default(Client)))
                valid = recipientClient.UpdateBalance(value + fare, TransactionFlow.Payment);

            if (!valid)
                throw new Exception("Conta de destino não tem saldo suficiente para a transação");

            if (!(originClient is default(Client)))
                originClient.UpdateBalance(value, TransactionFlow.Deposit);

            _clientRepository.Save();
        }
    }
}