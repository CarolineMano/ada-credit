using AdaCredit.Enum;

namespace AdaCredit.Entities
{
    public class Transaction
    {
        public string OriginBankId { get; protected set; }
        public string OriginBankBranch { get; protected set; }
        public string OriginBankAccountNumber { get; protected set; }
        public string RecipientBankId { get; protected set; }
        public string RecipientBankBranch { get; protected set; }
        public string RecipientBankAccountNumber { get; protected set; }
        public TransactionType TransactionType { get; protected set; }
        public TransactionFlow TransactionFlow { get; protected set; }
        public decimal Value { get; protected set; }
        
        public Transaction(string originBankId, string originBankBranch, string originBankAccountNumber, string recipientBankId, string recipientBankBranch, string recipientBankAccountNumber, TransactionType transactionType, TransactionFlow transactionFlow, decimal value)
        {
            OriginBankId = originBankId;
            OriginBankBranch = originBankBranch;
            OriginBankAccountNumber = originBankAccountNumber;
            RecipientBankId = recipientBankId;
            RecipientBankBranch = recipientBankBranch;
            RecipientBankAccountNumber = recipientBankAccountNumber;
            TransactionType = transactionType;
            TransactionFlow = transactionFlow;
            Value = value;
        }
    }
}