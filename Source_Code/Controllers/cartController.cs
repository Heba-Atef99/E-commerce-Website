using E_commerce.Models;
using E_Commerce.View_Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    public class cartController : Controller
    {
        private readonly ICartRepository _CartRepository;

        private readonly IAccountRepository _AccountRepository;
        private readonly IItemRepository _itemRepository;

        public cartController(ICartRepository CartRepository, IAccountRepository AccountRepository, IItemRepository itemRepository)
        {
            _CartRepository = CartRepository;
            _AccountRepository = AccountRepository;
            _itemRepository = itemRepository;


        }
        public IActionResult Index()
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            List<ITEM> item_list = new List<ITEM>();
            IEnumerable<CART> b = _CartRepository.GetCartByAccId(account_id);
            foreach (CART cart in b)
            {
                item_list.Add(_itemRepository.GetItemById(cart.Item_Id));
               
            }
            List<CART> cart_list = (List<CART>)b;
           
            var mytuple = Tuple.Create(cart_list, item_list, a);




            return View(mytuple);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeCartItem(ITEM obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            _CartRepository.DeleteItemFromCart(account_id, obj.Id);

            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProceedToCheckOut(ITEM obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT acc = _AccountRepository.GetAccountByAccId(account_id);
            IEnumerable<CART> a= _CartRepository.GetCartByAccId(account_id);
            int counter = 0;
            List<ITEM> item_list = new List<ITEM>();
            foreach (CART cart in a)
            {
                item_list.Add(_itemRepository.GetItemById(cart.Item_Id));
            }

            foreach (var obj2 in item_list)
            {
                counter = counter + obj2.Price;
            }
            if (acc.Balance<=counter)
            {
                ACCOUNT entity = _AccountRepository.GetAccountByAccId(account_id);
                entity.Id = account_id;
                entity.Email = acc.Email;
                entity.Pass = acc.Pass;
                entity.Balance = acc.Balance - counter;
                entity.User_Id = acc.User_Id;
                _AccountRepository.UpdateAccount(entity, 2);
                _CartRepository.DeleteAllCart(account_id);
                ViewBag.messageError = "Thank you for shopping with us";


            }
            else
            {
                ViewBag.messageError = "not enough balance!";
               
            }
          

            return View("Index");
        }
    }
}
