using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLCartRepository : ICartRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLCartRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //create
        public void AddToCart(CART c)
        {
            int maxId = 0;
            if (c.Account_Id % 2 == 0)
            {
                maxId = _sadb.CART.Select(i => i.Id).DefaultIfEmpty().Max();
                c.Id = (maxId > 0) ? maxId + 2 : 2;
                _sadb.CART.Add(c);
                _sadb.SaveChanges();
            }
            else
            {
                maxId = _apdb.CART.Select(i => i.Id).DefaultIfEmpty().Max();
                c.Id = (maxId > 0) ? maxId + 2 : 1;

                _apdb.CART.Add(c);
                _apdb.SaveChanges();
            }
        }

        //read
        public IEnumerable<CART> GetCartByAccId(int id)
        {
            IEnumerable<CART> cart;
            if (id % 2 == 0)
                cart = _sadb.CART.Where(i => i.Account_Id == id).ToList();
            
            else
                cart = _apdb.CART.Where(i => i.Account_Id == id).ToList();

            return cart;
        }


        //update

        //delete
        public Boolean DeleteAllCart(int accId)
        {
            if (accId % 2 == 0)
            {

                List<CART> c = _sadb.CART.Where(i => i.Account_Id == accId).ToList();
                if (c.Any())
                {
                    _sadb.CART.RemoveRange(c);
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                List<CART> c = _apdb.CART.Where(i => i.Account_Id == accId).ToList();
                if (c.Any())
                {
                    _apdb.CART.RemoveRange(c);
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Boolean DeleteItemFromCart(int accId, int itemId)
        {
            if (accId % 2 == 0)
            {
                CART i = _sadb.CART.Where(i => i.Account_Id == accId && i.Item_Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _sadb.CART.Remove(i);
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                CART i = _apdb.CART.Where(i => i.Account_Id == accId && i.Item_Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _apdb.CART.Remove(i);
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
