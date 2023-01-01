using AdaCredit.Entities;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace AdaCredit.Mapper
{
    public class TransactionFailedMap : ClassMap<TransactionFailed>
    {
        public TransactionFailedMap()
        {
            Map(m => m.Transaction.OriginBankId).Index(0);
            Map(m => m.Transaction.OriginBankBranch).Index(1);
            Map(m => m.Transaction.OriginBankAccountNumber).Index(2);
            Map(m => m.Transaction.RecipientBankId).Index(3);
            Map(m => m.Transaction.RecipientBankBranch).Index(4);
            Map(m => m.Transaction.RecipientBankAccountNumber).Index(5);
            Map(m => m.Transaction.TransactionType).Index(6);
            Map(m => m.Transaction.TransactionFlow).Index(7);
            Map(m => m.Transaction.Value).Index(8);
            Map(m => m.ErrorDetail).Index(9);
        }
    }
}