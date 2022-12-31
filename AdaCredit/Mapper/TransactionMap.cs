using AdaCredit.Entities;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace AdaCredit.Mapper
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.OriginBankId).Index(0);
            Map(m => m.OriginBankBranch).Index(1);
            Map(m => m.OriginBankAccountNumber).Index(2);
            Map(m => m.RecipientBankId).Index(3);
            Map(m => m.RecipientBankBranch).Index(4);
            Map(m => m.RecipientBankAccountNumber).Index(5);
            Map(m => m.TransactionType).Index(6);
            Map(m => m.TransactionFlow).Index(7);
            Map(m => m.Value).Index(8);
        }
    }
}