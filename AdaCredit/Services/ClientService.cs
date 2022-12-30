using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Persistence;

namespace AdaCredit.Services
{
    public class ClientService
    {
        private static ClientRepository _clientRepository = new ClientRepository();

        public void AddNewClient(string name, string document)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("Nome não pode ser vazio");

            if (!IsDocumentValid(document))
                throw new Exception("CPF informado não é válido");

            if (IsDocumentAlreadyUsed(document))
                throw new Exception("Esse CPF já está cadastrado");

            var newClient = CreateValidAccountNumber(name, document);

            _clientRepository.AddNewClient(newClient);

            _clientRepository.Save();
        }

        private Client CreateValidAccountNumber(string name, string document)
        {
            var validAccount = _clientRepository.IsPersistanceEmpty();
            Client client;

            if (validAccount)
                return new Client(name, document);

            do
            {
                client = new Client(name, document);

                validAccount = _clientRepository.GetByAccountNumber(client.Account.Number) is default(Client) ? true : false;
            } while (!validAccount);

            return client;
        }

        private bool IsDocumentValid(string document)
        {
            if (document.Length != 11)
                return false;

            if (!long.TryParse(document, out var documentValid))
                return false;

            return true;
        }

        private bool IsDocumentAlreadyUsed(string document)
        {
            return _clientRepository.GetByDocument(document) is default(Client) ? false : true;
        }
    }
}