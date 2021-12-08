using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using E_commerce.Models.Repositories;
using E_commerce.Models;

namespace E_commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IItemRepository _itemRepo;
        private readonly IPromotedItemRepository _PromotedItemRepo;
        private readonly ICartRepository _CartRepo;
        private readonly IAccountRepository _AccountRepository;
        public AccountController(IItemRepository itemRepository,
            IPromotedItemRepository PromotedItemRepoRepository, ICartRepository CartRepository,
            IAccountRepository AccountRepository)
        {
            _itemRepo = itemRepository;
            _PromotedItemRepo = PromotedItemRepoRepository;
            _CartRepo = CartRepository;
            _AccountRepository = AccountRepository;
        }
         
        public IActionResult Owner(int add, int share)
        {
            int loginaccount = 1;
            int owneraccount = 3;
            
            PROMOTED_ITEM promoted = new PROMOTED_ITEM();
            CART cart = new CART();
            if (share != 0)
            {
                
                promoted.Item_Id = share;
                promoted.Promoted_Account_Id = loginaccount;
                _PromotedItemRepo.AddPromotedItem(promoted);
            }
            if (add != 0)
            {
                cart.Item_Id = add;
                cart.Account_Id = loginaccount;
                cart.Item_count = 1;
                _CartRepo.AddToCart(cart);
            }

            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(owneraccount);
            IEnumerable<PROMOTED_ITEM> p= _PromotedItemRepo.GetPromotedItemsByAccId(owneraccount);
            var result = p.Select(O => O.Item_Id)
                        .ToList();
            List<ITEM> PromotedItem = new List<ITEM>();
            ITEM l = new ITEM();
            for (int i = 0; i < result.Count; i++)
            {

                l = _itemRepo.GetItemById(result[i]);
                 PromotedItem.Add(l);
            }


            ViewBag.data1 = t;
            ViewBag.data2 = PromotedItem;
            
            return View();
        }
        public IActionResult Deposit(string btn,string add)
        {
            string t = "";
            int loginaccount = 1;
            ACCOUNT account = _AccountRepository.GetAccountByAccId(loginaccount);
            //int balance = account.Balance;
            
            if (btn != null&&add != null)
            {
                account.Balance = account.Balance + int.Parse(add);
                bool k = _AccountRepository.UpdateAccount(account, 0);

            }
            
            ViewBag.deposit = account.Balance;
            return View();
        }
       

    }
}
