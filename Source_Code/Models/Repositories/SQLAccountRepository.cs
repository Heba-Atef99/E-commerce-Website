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
        public int AddAccount(ACCOUNT a)
        {
            List<ACCOUNT2> check = _apdb.ACCOUNT.Where(i => i.User_Id == a.User_Id).ToList();
            if (check.Any() == false)
            {
                List<int> maxId = _apdb.ACCOUNT.Select(i => i.Id).ToList();
                maxId.Sort();
                int id = (maxId.Any() == true) ? maxId.Last() + 1 : 1;
                //email & Pass on sadb
                ACCOUNT1 a1 = new ACCOUNT1 { Id = id, Email = a.Email, Pass = a.Pass };
                ACCOUNT2 a2 = new ACCOUNT2 { Id = id, User_Id = a.User_Id, Balance = a.Balance };
                _sadb.ACCOUNT.Add(a1);
                _apdb.ACCOUNT.Add(a2);

                _sadb.SaveChanges();
                _apdb.SaveChanges();
                return id;
            }
            return 0;
        }

        //Read
        public ACCOUNT GetAccountByAccId(int accId)
        {
            ACCOUNT1 acc1 = _sadb.ACCOUNT.Where(a => a.Id == accId).Single();
            ACCOUNT2 acc2 = _apdb.ACCOUNT.Where(a => a.Id == accId).Single();

            ACCOUNT acc = new ACCOUNT { Id = accId, Email = acc1.Email, Pass = acc1.Pass, Balance = acc2.Balance, User_Id = acc2.User_Id};
            return acc;
        }

        public ACCOUNT GetAccountByUserId(int userId)
        {
            ACCOUNT2 acc2 = _apdb.ACCOUNT.Where(a => a.User_Id == userId).Single();
            ACCOUNT1 acc1 = _sadb.ACCOUNT.Where(a => a.Id == acc2.Id).Single();
            ACCOUNT acc = new ACCOUNT { Id = acc2.Id, Email = acc1.Email, Pass = acc1.Pass, Balance = acc2.Balance, User_Id = acc2.User_Id };
            return acc;
        }

        public IEnumerable<ACCOUNT1> GetAllAccountEmailsAndPass()
        {

            IEnumerable<ACCOUNT1> accounts = _sadb.ACCOUNT.ToList();
            return accounts;
        }

        public ACCOUNT GetAccountByEmail(string email)
        {
            ACCOUNT1 acc1 = _sadb.ACCOUNT.Where(a => a.Email == email).Single();
            ACCOUNT2 acc2 = _apdb.ACCOUNT.Where(a => a.Id == acc1.Id).Single();
            ACCOUNT acc = new ACCOUNT { Id = acc2.Id, Email = acc1.Email, Pass = acc1.Pass, Balance = acc2.Balance, User_Id = acc2.User_Id };
            return acc;
        }
        //Update
        public Boolean UpdateAccount(ACCOUNT updatedAcc, int isEmailorPassUpdated)
        {
            ACCOUNT1 exist1;
            ACCOUNT2 exist2;

            if (isEmailorPassUpdated == 1)
            {
                exist1 = _sadb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                if (exist1 != null)
                {
                    exist1.Email = updatedAcc.Email;
                    exist1.Pass = updatedAcc.Pass;
                    var modified = _sadb.ACCOUNT.Attach(exist1);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();
                    return true;
                } 
            }
            else if (isEmailorPassUpdated == 0)
            {
                exist2 = _apdb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                if (exist2 != null)
                {
                    exist2.Balance = updatedAcc.Balance;
                    exist2.User_Id = updatedAcc.User_Id;
                    var modified = _apdb.ACCOUNT.Attach(exist2);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            else
            {
                exist1 = _sadb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                exist2 = _apdb.ACCOUNT.Where(i => i.Id == updatedAcc.Id).FirstOrDefault();
                if (exist1 != null && exist2 != null)
                {
                    exist1.Email = updatedAcc.Email;
                    exist1.Pass = updatedAcc.Pass;
                    exist2.Balance = updatedAcc.Balance;
                    exist2.User_Id = updatedAcc.User_Id;

                    var modified1 = _sadb.ACCOUNT.Attach(exist1);
                    modified1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();

                    var modified2 = _apdb.ACCOUNT.Attach(exist2);
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
            ACCOUNT2 a2 = _apdb.ACCOUNT.Where(i => i.User_Id == userId).FirstOrDefault();
            ACCOUNT1 a1 = _sadb.ACCOUNT.Where(i => i.Id == a2.Id).FirstOrDefault();
            if (a1 != null && a2 != null)
            {
                _sadb.ACCOUNT.Remove(a1);
                _apdb.ACCOUNT.Remove(a2);
                _sadb.SaveChanges();
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteAccountById(int accId)
        {
            ACCOUNT2 a2 = _apdb.ACCOUNT.Where(i => i.Id == accId).FirstOrDefault();
            ACCOUNT1 a1 = _sadb.ACCOUNT.Where(i => i.Id == accId).FirstOrDefault();
            if (a1 != null && a2 != null)
            {
                _sadb.ACCOUNT.Remove(a1);
                _apdb.ACCOUNT.Remove(a2);
                _sadb.SaveChanges();
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
