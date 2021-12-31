using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface ITransactionHistoryRepository
    {
        public void AddTransaction(TRANSACTION_HISTORY i);
        public IEnumerable<TRANSACTION_HISTORY> GetTransactionsBySenderId(int accId);
        public IEnumerable<TRANSACTION_HISTORY> GetTransactionsByReceiverId(int accId);
        public Boolean DeleteTransactionById(int id);

    }
}
