using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLTransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLTransactionHistoryRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //create
        public void AddTransaction(TRANSACTION_HISTORY i)
        {
            int maxId = _apdb.TRANSACTION_HISTORY.Select(a => a.Id).DefaultIfEmpty().Max();
            //email & Pass on sadb
            TRANSACTION1 t1 = new TRANSACTION1 { Id = maxId + 1, Money = i.Money, Receiver_Id = i.Receiver_Id };
            TRANSACTION2 t2 = new TRANSACTION2 { Id = maxId + 1, Sender_Id = i.Sender_Id };
            _sadb.TRANSACTION_HISTORY.Add(t1);
            _apdb.TRANSACTION_HISTORY.Add(t2);

            _sadb.SaveChanges();
            _apdb.SaveChanges();
        }

        //read
        public IEnumerable<TRANSACTION_HISTORY> GetTransactionsBySenderId(int accId)
        {
            List<TRANSACTION2> t2 = _apdb.TRANSACTION_HISTORY.Where(a => a.Sender_Id == accId).ToList();
            List<TRANSACTION1> t1 = _sadb.TRANSACTION_HISTORY.ToList();

            IEnumerable<TRANSACTION_HISTORY> TH = t2.Join(t1, i1 => i1.Id, i2 => i2.Id, 
                (i1, i2) => new TRANSACTION_HISTORY
                { 
                    Id = i1.Id, 
                    Sender_Id = i1.Sender_Id,
                    Money = i2.Money,
                    Receiver_Id = i2.Receiver_Id
                }).ToList();
            return TH;
        }

        public IEnumerable<TRANSACTION_HISTORY> GetTransactionsByReceiverId(int accId)
        {
            List<TRANSACTION2> t2 = _apdb.TRANSACTION_HISTORY.ToList();
            List<TRANSACTION1> t1 = _sadb.TRANSACTION_HISTORY.Where(a => a.Receiver_Id == accId).ToList();

            IEnumerable<TRANSACTION_HISTORY> TH = t2.Join(t1, i1 => i1.Id, i2 => i2.Id,
                (i1, i2) => new TRANSACTION_HISTORY
                {
                    Id = i1.Id,
                    Sender_Id = i1.Sender_Id,
                    Money = i2.Money,
                    Receiver_Id = i2.Receiver_Id
                }).ToList();
            return TH;
        }
        //update


        //delete
        public Boolean DeleteTransactionById(int id)
        {
            TRANSACTION2 t2 = _apdb.TRANSACTION_HISTORY.Where(i => i.Id == id).FirstOrDefault();
            TRANSACTION1 t1 = _sadb.TRANSACTION_HISTORY.Where(i => i.Id == id).FirstOrDefault();
            if (t1 != null && t2 != null)
            {
                _sadb.TRANSACTION_HISTORY.Remove(t1);
                _apdb.TRANSACTION_HISTORY.Remove(t2);
                _sadb.SaveChanges();
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
