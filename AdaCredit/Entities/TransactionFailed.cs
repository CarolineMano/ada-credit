using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Enum;

namespace AdaCredit.Entities
{
    public class TransactionFailed
    {
        public Transaction Transaction { get; set; }
        public string ErrorDetail { get; private set; }

        public TransactionFailed(Transaction transaction, string errorDetail)
        {
            Transaction = transaction;
            ErrorDetail = errorDetail;
        }
    }
}