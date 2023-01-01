using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AdaCredit.Entities;
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

        public List<Transaction> GetTransactionsFromFile(string fileName)
        {
            using (var reader = new StreamReader(@$"{_desktopPath}\Transactions\Pending\{fileName}"))
            using (var csv = new CsvReader(reader, _config))
            {
                return csv.GetRecords<Transaction>().ToList();
            }
        }

        public Stack<String> GetFileNames()
        {
            var fileNames = new Stack<String>();

            DirectoryInfo directoryInfo = new DirectoryInfo(@$"{_desktopPath}\Transactions\Pending");

            FileInfo[] files = directoryInfo.GetFiles("*.csv");

            foreach (var file in files)
            {
                fileNames.Push(file.Name);
            }

            return fileNames;
        }

        public void SaveCompleted(List<Transaction> transactionsCompleted, string fileName)
        {
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