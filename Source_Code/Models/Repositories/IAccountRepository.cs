using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IAccountRepository
    {
        public int AddAccount(ACCOUNT a);
        public IEnumerable<ACCOUNT1> GetAllAccountEmailsAndPass();
        public ACCOUNT GetAccountByAccId(int accId);
        public ACCOUNT GetAccountByUserId(int userId);
        public Boolean UpdateAccount(ACCOUNT updatedAcc, int isEmailorPassUpdated);
        public Boolean DeleteAccountByUserId(int userId);
        public Boolean DeleteAccountById(int accId);
    }
}
