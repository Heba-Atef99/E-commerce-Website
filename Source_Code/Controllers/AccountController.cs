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
    public class compositeitem
    {
        public ITEM item;
        public string owner;
        public string type;
        public compositeitem(ITEM t, string name,string typename)
        {
            item = t;
            owner = name;
            type = typename;
        }
    }
    public class AccountController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPromotedItemRepository _PromotedItemRepository;
        private readonly ICartRepository _CartRepository;
        private readonly IAccountRepository _AccountRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ITypeRepository _TypeRepository;
        public AccountController(IItemRepository itemRepository,
            IPromotedItemRepository PromotedItemRepoRepository, ICartRepository CartRepository,
            IAccountRepository AccountRepository, IUserRepository UserRepository,
            ITypeRepository TypeRepository)
        {
            _itemRepository = itemRepository;
            _PromotedItemRepository = PromotedItemRepoRepository;
            _CartRepository = CartRepository;
            _AccountRepository = AccountRepository;
            _UserRepository = UserRepository;
            _TypeRepository = TypeRepository;
        } 
         
        public IActionResult Owner(int add, int share)
        {
            //int loginaccount = 3;
            int loginaccount = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            //int owneraccount = 1;
            int owneraccount = (int)HttpContext.Session.GetInt32("owner_id");

            PROMOTED_ITEM promoted = new PROMOTED_ITEM();
            CART cart = new CART();
            if (share != 0)
            {
                
                promoted.Item_Id = share;
                promoted.Promoted_Account_Id = loginaccount;
                _PromotedItemRepository.AddPromotedItem(promoted);
            }
            if (add != 0)
            {
                cart.Item_Id = add;
                cart.Account_Id = loginaccount;
                cart.Item_count = 1;
                _CartRepository.AddToCart(cart);
            }

            IEnumerable<ITEM> t = _itemRepository.GetAvailableItemsByAccId(owneraccount);
            //List<string> owner_name = new List<string>();
            List<compositeitem> cat = new List<compositeitem>();
            foreach (var k in t)
            {
                ITEM item = k;
                int account_id = item.Owner_Account_Id;
                int type_id = item.Type;
                string type= _TypeRepository.GetTypeById(type_id).Type;
                var user_id = _AccountRepository.GetAccountByAccId(account_id).User_Id;
                string name = _UserRepository.GetUserById(user_id).Name;
                compositeitem f = new compositeitem(item, name, type);
                cat.Add(f);
            }
            IEnumerable<PROMOTED_ITEM> p = _PromotedItemRepository.GetPromotedItemsByAccId(owneraccount);
            var result = p.Select(O => O.Item_Id)
                        .ToList();
            List<ITEM> PromotedItem = new List<ITEM>();
            ITEM l = new ITEM();
            for (int i = 0; i < result.Count; i++)
            {

                l = _itemRepository.GetItemById(result[i]);
                if (l.Status == 1)
                {
                    PromotedItem.Add(l);
                }
                
            }
            List<compositeitem> cat2= new List<compositeitem>();
            foreach (var k in PromotedItem)
            {
                ITEM item = k;
                int account_id = item.Owner_Account_Id;
                int type_id = item.Type;
                string type = _TypeRepository.GetTypeById(type_id).Type;
                var user_id = _AccountRepository.GetAccountByAccId(account_id).User_Id;
                string name = _UserRepository.GetUserById(user_id).Name;
                compositeitem f = new compositeitem(item, name, type);
                cat2.Add(f);
            }

            //List<TYPE> typeitem = new List<TYPE>();
            //TYPE j = new TYPE();
            //foreach(var item in t)
            //{
            //    j= _TypeRepository.GetTypeById(item.Id);
            //    typeitem.Add(j);
            //}
            //var patient_join_follow_up = new List<Tuple<ITEM, TYPE>>();
            //int k = 0;
            //foreach ( ITEM  f in t)
            //{


            //    patient_join_follow_up.Add(new Tuple<ITEM, TYPE>(f, typeitem[k]));
            //    ++k;
            //}

            //List<string> owner_name = new List<string>();
            //foreach (var item in t)
            //{
            //    int account_id = item.Owner_Account_Id;
            //    var user_id = _AccountRepository.GetAccountByAccId(account_id).User_Id;
            //    string name = _UserRepository.GetUserById(user_id).Name;
            //    owner_name.Add(name);
            //}
            //ViewBag.owners = owner_name;


            ViewBag.data1 = cat;
            ViewBag.data2 = cat2;
            
            return View();
        }
        public IActionResult Deposit(string btn,string add)
        {
            
            //int loginaccount = 1;
            int loginaccount = (int)HttpContext.Session.GetInt32("User_Reg_Id");
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
