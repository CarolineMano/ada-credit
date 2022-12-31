using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdaCredit.Dtos;
using AdaCredit.Entities;
using AdaCredit.Persistence;

namespace AdaCredit.Services
{
    public class ClientService
    {
        private static ClientRepository _clientRepository = new ClientRepository();

        public void AddNewClient(string name, string document, string email)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("Nome não pode ser vazio");

            if (!IsDocumentValid(document))
                throw new Exception("CPF informado não é válido");

            if (!IsEmailValid(email))
                throw new Exception("Email em formato inválido");

            if (IsDocumentAlreadyUsed(document))
                throw new Exception("Esse CPF já está cadastrado");

            var newClient = CreateValidAccountNumber(name, document, email);

            _clientRepository.AddNewClient(newClient);

            _clientRepository.Save();
        }

        public ClientDto GetClientByDocument(string document)
        {
            if (!IsDocumentValid(document))
                throw new Exception("CPF informado não é válido");

            if (!IsDocumentAlreadyUsed(document))
                throw new Exception("Esse CPF não está cadastrado");

            var client = _clientRepository.GetByDocument(document);

            return new ClientDto()
            {
                Name = client.Name,
                Document = client.Document,
                Account = client.Account,
                Active = client.Active,
                Email = client.Email,
                Balance = client.Balance
            };
        }

        public ClientDto GetClientByAccountNumber(string accountNumber)
        {
            if (!IsAccountNumberValid(accountNumber))
                throw new Exception("Número da conta inválido. Deve seguir o padrão #####-#");

            var client = _clientRepository.GetByAccountNumber(accountNumber);

            if (client is default(Client))
                throw new Exception("Número de conta inexistente.");

            return new ClientDto()
            {
                Name = client.Name,
                Document = client.Document,
                Account = client.Account,
                Active = client.Active,
                Email = client.Email,
                Balance = client.Balance
            };
        }

        public bool DeleteClient(string document)
        {
            if (!IsDocumentValid(document))
                throw new Exception("CPF informado não é válido");

            var client = _clientRepository.GetByDocument(document);

            if (!client.Active)
                throw new Exception("Cliente informado já está inativo");

            client.Disable();
            _clientRepository.Save();

            return true;
        }

        public bool UpdateClient(string document, string email)
        {
            var client = _clientRepository.GetByDocument(document);

            if (!IsEmailValid(email))
                throw new Exception("Email informado é inválido.");

            if (client is default(Client))
                throw new Exception("Esse CPF não está cadastrado.");

            client.Email = email;

            _clientRepository.Save();

            return true;
        }

        private Client CreateValidAccountNumber(string name, string document, string email)
        {
            var validAccount = _clientRepository.IsPersistanceEmpty();
            Client client;

            if (validAccount)
                return new Client(name, document, email);

            do
            {
                client = new Client(name, document, email);

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

        private bool IsAccountNumberValid(string accountNumber)
        {
            string pattern = @"^[0-9]{5}-[0-9]{1}$";

            Regex reg = new Regex(pattern);
            return reg.IsMatch(accountNumber);
        }

        private bool IsEmailValid(string email)
        {
            string pattern = @"[a-zA-z0-9\.]+@[a-zA-z0-9\.]+\.[A-Za-z]+";

            Regex reg = new Regex(pattern);
            return reg.IsMatch(email);
        }
    }
}