using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;

namespace AdaCredit.Dtos
{
    public class ClientDto : PersonDto
    {
        public string? Document { get; set; }
        public Account? Account { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }

        public ClientDto(Client client)
        {
            Name = client.Name;
            Document = client.Document;
            Account = client.Account;
            Active = client.Active;
            Email = client.Email;
            Balance = client.Balance;
        }

        public override string ToString()
        {
            return $"Nome: {Name} {Environment.NewLine}CPF: {Document}{Environment.NewLine}Email: {Email}" +
                $"{Environment.NewLine}Agência: {Account.Branch} Conta: {Account.Number}{Environment.NewLine}Ativo: {(Active ? "Ativo" : "Inativo")}" +
                $"{Environment.NewLine}Saldo: {Balance.ToString("C")}";
        }
    }
}