using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Enum;

namespace AdaCredit.Entities
{
    public class TransactionFailed : Transaction
    {
        public string ErrorDetail { get; private set; }

        public TransactionFailed(string originBankId, string originBankBranch, string originBankAccountNumber, string recipientBankId, string recipientBankBranch, string recipientBankAccountNumber, TransactionType transactionType, TransactionFlow transactionFlow, decimal value, string errorDetail) : base(originBankId, originBankBranch, originBankAccountNumber, recipientBankId, recipientBankBranch, recipientBankAccountNumber, transactionType, transactionFlow, value)
        {
            ErrorDetail = errorDetail;
        }

        public override string ToString()
        {
            return $"Banco de Origem: {this.OriginBankId}{Environment.NewLine}" +
                    $"Agência de Origem: {this.OriginBankBranch}{Environment.NewLine}" +
                    $"Conta de Origem: {this.OriginBankAccountNumber}{Environment.NewLine}" +
                    $"Banco de Destino: {this.RecipientBankId}{Environment.NewLine}" +
                    $"Agência de Destino: {this.RecipientBankBranch}{Environment.NewLine}" +
                    $"Conta de Destino: {this.RecipientBankAccountNumber}{Environment.NewLine}" +
                    $"Tipo de Transação: {this.TransactionType}{Environment.NewLine}" +
                    $"Fluxo da Transação: {this.TransactionFlow}{Environment.NewLine}" +
                    $"Valor da Transação: {this.Value.ToString("C")}{Environment.NewLine}" +
                    $"Detalhe do Erro: {this.ErrorDetail}{Environment.NewLine}";
        }
    }
}