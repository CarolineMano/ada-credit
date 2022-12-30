using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Mapper;
using CsvHelper;
using CsvHelper.Configuration;

namespace AdaCredit.Persistence
{
    public class ClientRepository
    {
        private static List<Client> _clients;
        private static CsvConfiguration _config;

        static ClientRepository()
        {
            _config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null
            };

            Initialize();
        }

        private static void Initialize()
        {
            try
            {
                using (var reader = new StreamReader("../AdaCredit/Database/Clients.csv"))
                using (var csv = new CsvReader(reader, _config))
                {
                    _clients = csv.GetRecords<Client>().ToList();
                }
            }
            catch (System.Exception)
            {
                using (var writer = new StreamWriter("../AdaCredit/Database/Clients.csv"))
                using (var csv = new CsvWriter(writer, _config))
                {
                    csv.Context.RegisterClassMap<ClientMap>();
                    csv.WriteHeader<Client>();
                }
                _clients = new List<Client>();
            }
        }

        public Client? GetByAccountNumber(string accountNumber)
        {
            return _clients.FirstOrDefault(c => c.Account.Number == accountNumber);
        }

        public Client? GetByDocument(string document)
        {
            return _clients.FirstOrDefault(c => c.Document == document);
        }

        public void AddNewClient(Client client)
        {
            _clients.Add(client);
        }

        public bool IsPersistanceEmpty()
        {
            return _clients.Count == 0 ? true : false;
        }

        public void Save()
        {
            using (var writer = new StreamWriter("../AdaCredit/Database/Clients.csv"))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<ClientMap>();
                csv.WriteRecords<Client>(_clients);
            }
        }
    }
}