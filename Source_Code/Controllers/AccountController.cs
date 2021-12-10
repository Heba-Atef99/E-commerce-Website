using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using E_commerce.Models.Repositories;
using E_commerce.Models;
using E_Commerce.View_Models;

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
        public IActionResult Profile()
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER b = _UserRepository.GetUserById(a.User_Id);

            var mytuple = Tuple.Create(a, b);

            return View(mytuple);
        }
        public IActionResult ChangeName()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeName(USER obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER entity = _UserRepository.GetUserById(a.User_Id);
            entity.Id = a.User_Id;
            entity.Name = obj.Name;
            entity.Address = obj.Address;
            entity.Phone = obj.Phone;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

            _UserRepository.UpdateUserName(a.User_Id, entity);

            return RedirectToAction("Profile");

        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(S obj)
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);

            if (BCrypt.Net.BCrypt.Verify(obj.currentPaasword, a.Pass))
            {
                if (obj.newPassword == obj.reenterNewPassword)
                {
                    a.Pass = BCrypt.Net.BCrypt.HashPassword(obj.newPassword);
                    ACCOUNT entity = _AccountRepository.GetAccountByAccId(a.Id);
                    entity.Id = a.Id;
                    entity.Email = a.Email;
                    entity.Pass = a.Pass;
                    entity.Balance = a.Balance;
                    entity.User_Id = a.User_Id;

                    _AccountRepository.UpdateAccount(entity, 1);


                }
                else
                {
                    ViewBag.messageError = "Passwords don't match";
                    return View();
                }
            }
            else
            {
                ViewBag.messageError = "wrong password";
                return View();
            }

            return RedirectToAction("Profile");

        }
        public IActionResult ChangeEmail()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeEmail(ACCOUNT obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT entity = _AccountRepository.GetAccountByAccId(account_id);
            entity.Id = account_id;
            entity.Email = obj.Email;
            entity.Pass = obj.Pass;
            entity.Balance = obj.Balance;
            entity.User_Id = obj.User_Id;
            //entity.USER =_UserRepository.GetUserById(entity.User_Id);
            _AccountRepository.UpdateAccount(entity, 1);


            return RedirectToAction("Profile");

        }
        public IActionResult ChangeAddress()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeAddress(USER obj)
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER entity = _UserRepository.GetUserById(a.User_Id);
            entity.Id = a.User_Id;
            entity.Name = obj.Name;
            entity.Address = obj.Address;
            entity.Phone = obj.Phone;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

            _UserRepository.UpdateUser(entity);

            return RedirectToAction("Profile");

        }
        public IActionResult ChangePhone()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePhone(USER obj)
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER entity = _UserRepository.GetUserById(a.User_Id);
            entity.Id = a.User_Id;
            entity.Name = obj.Name;
            entity.Address = obj.Address;
            entity.Phone = obj.Phone;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

            _UserRepository.UpdateUser(entity);

            return RedirectToAction("Profile");

        }
        public IActionResult Cart()
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
        public IActionResult RemoveCartItem(ITEM obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            _CartRepository.DeleteItemFromCart(account_id, obj.Id);

            return View("Cart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProceedToCheckOut(ITEM obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT acc = _AccountRepository.GetAccountByAccId(account_id);
            IEnumerable<CART> a = _CartRepository.GetCartByAccId(account_id);
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
            if (acc.Balance <= counter)
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


            return View("Cart");
        }


    }
}
