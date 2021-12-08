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
    public class OwnerController : Controller
    {
        private readonly IItemRepository _itemRepo;
        private readonly IPromotedItemRepository _PromotedItemRepo;
        private readonly ICartRepository _CartRepo;

        public OwnerController(IItemRepository itemRepository, IPromotedItemRepository PromotedItemRepoRepository, ICartRepository CartRepository)
        {
            _itemRepo = itemRepository;
            _PromotedItemRepo = PromotedItemRepoRepository;
            _CartRepo = CartRepository;
        }
         
        public IActionResult Owner(int add, int share)
        {
            int loginaccount = 2;
            int owneraccount = 1;
            
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
                _CartRepo.AddToCart(cart);
            }

            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(owneraccount);
            IEnumerable<PROMOTED_ITEM> p= _PromotedItemRepo.GetPromotedItemsByAccId(owneraccount);
            var result = p.Select(O => O.Item_Id)
                        .ToList();
            List<ITEM> PromotedItem = new List<ITEM>();
            for (int i = 0; i < result.Count; i++)
            {
                ITEM l = _itemRepo.GetItemById(i);
                PromotedItem.Add(l);
            }


            ViewBag.data1 = t;
            ViewBag.data2 = PromotedItem;
            
            return View();
        }
        public IActionResult Deposit(string btn)
        {
            int t = 0;
            if (btn != null)
            {
                 t = 10;
            }
            
            return View(t);
        }
       

    }
}
