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

        public override string ToString()
        {
            return $"Nome: {Name} {Environment.NewLine}CPF: {Document}{Environment.NewLine}Email: {Email}" +
                $"{Environment.NewLine}Agência: {Account.Branch} Conta: {Account.Number}{Environment.NewLine}Ativo: {Active}";
        }
    }
}