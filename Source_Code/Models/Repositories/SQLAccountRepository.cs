using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLAccountRepository : IAccountRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLAccountRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //Create
        public void AddAccount(ACCOUNT a)
        {
            int maxId = _apdb.ACCOUNT.Select(a => a.Id).DefaultIfEmpty().Max();
            //email & Pass on sadb
            ACCOUNT a1 = new ACCOUNT { Id = maxId + 1, Email = a.Email, Pass = a.Pass };
            ACCOUNT a2 = new ACCOUNT { Id = maxId + 1, User_Id = a.User_Id, Balance = a.Balance };
            _sadb.ACCOUNT.Add(a1);
            _apdb.ACCOUNT.Add(a2);

            _sadb.SaveChanges();
            _apdb.SaveChanges();
        }

        //Read
        public ACCOUNT GetAccountByAccId(int accId)
        {
            ACCOUNT acc = _sadb.ACCOUNT.Where(a => a.Id == accId).Single();
            ACCOUNT acc2 = _apdb.ACCOUNT.Where(a => a.Id == accId).Single();
            acc.User_Id = acc2.User_Id;
            acc.Balance = acc2.Balance;

            return acc;
        }

        public ACCOUNT GetAccountByUserId(int userId)
        {
            ACCOUNT acc2 = _apdb.ACCOUNT.Where(a => a.User_Id == userId).Single();
            ACCOUNT acc = _sadb.ACCOUNT.Where(a => a.Id == acc2.Id).Single();
            acc.User_Id = acc2.User_Id;
            acc.Balance = acc2.Balance;

            return acc;
        }

        public IEnumerable<ACCOUNT> GetAllAccountEmailsAndPass()
        {

            IEnumerable<ACCOUNT> accounts = _sadb.ACCOUNT;
            return accounts;
        }

        //Update
        public Boolean UpdateAccount(ACCOUNT updatedAcc, int isEmailorPassUpdated)
        {
            ACCOUNT exist;

            if (isEmailorPassUpdated == 1)
            {
                exist = _sadb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                if (exist != null)
                {

                    var modified = _sadb.ACCOUNT.Attach(updatedAcc);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();
                    return true;
                } 
            }
            else if (isEmailorPassUpdated == 0)
            {
                exist = _apdb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                if (exist != null)
                {

                    var modified = _apdb.ACCOUNT.Attach(updatedAcc);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            else
            {
                exist = _sadb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                ACCOUNT exist2 = _apdb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                if (exist != null && exist2 != null)
                {

                    var modified1 = _sadb.ACCOUNT.Attach(updatedAcc);
                    modified1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();

                    var modified2 = _apdb.ACCOUNT.Attach(updatedAcc);
                    modified2.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        //Delete
        public Boolean DeleteAccountByUserId(int userId)
        {
            ACCOUNT a = _apdb.ACCOUNT.Where(i => i.User_Id == userId).FirstOrDefault();
            ACCOUNT a2 = _sadb.ACCOUNT.Where(i => i.Id == a.Id).FirstOrDefault();
            if (a != null && a2 != null)
            {
                _sadb.ACCOUNT.Remove(a2);
                _apdb.ACCOUNT.Remove(a);
                _sadb.SaveChanges();
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteAccountById(int accId)
        {
            ACCOUNT a = _apdb.ACCOUNT.Where(i => i.Id == accId).FirstOrDefault();
            ACCOUNT a2 = _sadb.ACCOUNT.Where(i => i.Id == accId).FirstOrDefault();
            if (a != null && a2 != null)
            {
                _sadb.ACCOUNT.Remove(a2);
                _apdb.ACCOUNT.Remove(a);
                _sadb.SaveChanges();
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
