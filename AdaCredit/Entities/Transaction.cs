using AdaCredit.Enum;

namespace AdaCredit.Entities
{
    public sealed class Transaction
    {
        public string OriginBankId { get; private set; }
        public string OriginBankBranch { get; private set; }
        public string OriginBankAccountNumber { get; private set; }
        public string RecipientBankId { get; private set; }
        public string RecipientBankBranch { get; private set; }
        public string RecipientBankAccountNumber { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public TransactionFlow TransactionFlow { get; private set; }
        public decimal Value { get; private set; }
        
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