using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface ICartRepository
    {
        public void AddToCart(CART c);
        public IEnumerable<CART> GetCartByAccId(int id);
        public Boolean DeleteAllCart(int accId);
        public Boolean DeleteItemFromCart(int accId, int itemId);
    }
}
