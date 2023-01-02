using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdaCredit.Entities;
using AdaCredit.Enum;
using AdaCredit.Mapper;
using CsvHelper;
using CsvHelper.Configuration;

namespace AdaCredit.Persistence
{
    public class TransactionRepository
    {
        private static CsvConfiguration _config;
        private string _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public TransactionRepository()
        {
            _config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
        }

        public List<Transaction> GetTransactionsFromFile(TransactionFolder transactionFolder, string fileName)
        {
            using (var reader = new StreamReader(@$"{_desktopPath}\Transactions\{transactionFolder}\{fileName}"))
            using (var csv = new CsvReader(reader, _config))
            {
                return csv.GetRecords<Transaction>().ToList();
            }
        }

        public List<TransactionFailed> GetFailedTransactionsFromFile(TransactionFolder transactionFolder, string fileName)
        {
            using (var reader = new StreamReader(@$"{_desktopPath}\Transactions\{transactionFolder}\{fileName}"))
            using (var csv = new CsvReader(reader, _config))
            {
                return csv.GetRecords<TransactionFailed>().ToList();
            }
        }

        public Stack<String> GetFileNames(TransactionFolder transactionFolder)
        {
            var fileNames = new Stack<String>();

            DirectoryInfo directoryInfo = new DirectoryInfo(@$"{_desktopPath}\Transactions\{transactionFolder}");

            FileInfo[] files = directoryInfo.GetFiles("*.csv");
            
            var fileListSorted = files.Select(f => f.Name.Split("-")).OrderByDescending(f => f.Last()).ToList();

            foreach (var file in fileListSorted)
            {
                fileNames.Push(String.Join("-", file));
            }

            return fileNames;
        }

        public void SaveCompleted(List<Transaction> transactionsCompleted, string fileName)
        {
            fileName = fileName.Split(".csv")[0];
            var completedFolder = @$"{_desktopPath}\Transactions\Completed";
            bool folderExists = Directory.Exists(completedFolder);

            if (!folderExists)
                Directory.CreateDirectory(completedFolder);

            using (var writer = new StreamWriter(@$"{completedFolder}\{fileName}-completed.csv"))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<TransactionMap>();
                csv.WriteRecords<Transaction>(transactionsCompleted);
            }
        }

        public void SaveFailed(List<TransactionFailed> transactionsFailed, string fileName)
        {
            fileName = fileName.Split(".csv")[0];
            var failedFolder = @$"{_desktopPath}\Transactions\Failed";
            bool folderExists = Directory.Exists(failedFolder);

            if (!folderExists)
                Directory.CreateDirectory(failedFolder);

            using (var writer = new StreamWriter(@$"{failedFolder}\{fileName}-failed.csv"))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<TransactionFailedMap>();
                csv.WriteRecords<TransactionFailed>(transactionsFailed);
            }
        }
    }
}