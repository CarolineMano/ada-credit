using AdaCredit.Enum;

namespace AdaCredit.Entities
{
    public sealed class Transaction
    {
        public string OriginBankId { get; set; }
        public string OriginBankBranch { get; set; }
        public string OriginBankAccountNumber { get; set; }
        public string RecipientBankId { get; set; }
        public string RecipientBankBranch { get; set; }
        public string RecipientBankAccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionFlow TransactionFlow { get; set; }
        public decimal Value { get; set; }
        
        public Transaction(string originBankId, string originBankBranch, string originBankAccountNumber, string recipientBankId, string recipientBankBranch, string recipientBankAccountNumber, string transactionType, int transactionFlow, decimal value)
        {
            OriginBankId = originBankId;
            OriginBankBranch = originBankBranch;
            OriginBankAccountNumber = originBankAccountNumber;
            RecipientBankId = recipientBankId;
            RecipientBankBranch = recipientBankBranch;
            RecipientBankAccountNumber = recipientBankAccountNumber;
            TransactionType = Enum.TransactionType.Parse<TransactionType>(transactionType);
            TransactionFlow = (TransactionFlow)transactionFlow;
            Value = value;
        }
    }
}