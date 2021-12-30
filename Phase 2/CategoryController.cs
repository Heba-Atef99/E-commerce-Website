using E_commerce.Models;
using E_commerce.Models.Repositories;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace E_commerce.Controllers
{

    public class Catitem
    {
        public ITEM item;
        public string owner;
        public Catitem(ITEM t, string name)
        {
            item = t;
            owner = name;
        }
    }
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IItemRepository _item;
        private readonly IUserRepository _user;
        private readonly ICartRepository _cart;
        private readonly IAccountRepository _account;
        private readonly ITypeRepository _typeRepo;


        public CategoryController(IItemRepository item, IUserRepository user, ICartRepository cart, IAccountRepository account, ITypeRepository typeRepository)
        {
            _item = item;
            _user = user;
            _cart = cart;
            _account = account;
            _typeRepo = typeRepository;

        }

        [HttpGet]
        public ActionResult <IEnumerable<TYPE>> Main()
        {
            IEnumerable<TYPE> t = _typeRepo.GetAllTypes();
            
            return t.ToArray();
        }

            //return View();
        

        //public IActionResult CatItems(int add,int owner_id,int Sort, Search search_string)
        //{
            
        //    //int login_account=1;
        //    int login_account = (int)HttpContext.Session.GetInt32("User_Reg_Id");
        //    //int type_id = 1;
        //    int type_id = (int)HttpContext.Session.GetInt32("Type_Id");
        //    IEnumerable<ITEM> items = _item.GetAllItems();
        //    var type_exist = items.ToList().Any(u => u.Type == type_id);
        //    CART c = new CART();
        //    List<ITEM> sel_items = new List<ITEM>();
        //    sel_items = items.Where(u => u.Type == type_id && u.Status==1).OrderBy(i=>i.Name).ToList();

        //    //List<string> owner_name = new List<string>();
        //    List<Catitem> cat = new List<Catitem>();
        //    if (type_exist)
        //    {

        //        //for (int i = 0; i < 1; i++)
        //        //{
        //        //    ITEM item = sel_items[0];
        //        //    int st = item.Status;
        //        //    int account_id = item.Owner_Account_Id;
        //        //    var user_id = _account.GetAccountByAccId(account_id).User_Id;
        //        //    string name = _user.GetUserById(user_id).Name;
        //        //    Catitem f = new Catitem(item, name);
        //        //    cat.Add(f);
        //        //}
        //        if (search_string.Name != null)
        //        {
        //             sel_items = sel_items.Where(i => i.Name.Contains(search_string.Name)).ToList();
                    

        //        }
        //        if (search_string.Sort!=null)
        //        {
        //            sel_items = sel_items.OrderBy(i => i.Price).ToList();
        //            if(search_string.Sort =="DSC")
        //                sel_items = sel_items.OrderByDescending(i => i.Price).ToList();

        //        }
        //        foreach (var item in sel_items)
        //        {
        //            int account_id = item.Owner_Account_Id;
        //            var user_id = _account.GetAccountByAccId(account_id).User_Id;
        //            string name = _user.GetUserById(user_id).Name;
        //            Catitem f = new Catitem(item, name);
        //            cat.Add(f);

        //        }

        //        ViewBag.owners = cat;
        //        ViewBag.sitems = sel_items;

        //        if (add != 0)
        //        {
        //            c.Item_Id = add;
        //            c.Account_Id = login_account;
        //            c.Item_count = 1;
        //            _cart.AddToCart(c);
        //        }
        //        if (owner_id != 0)
        //        {
        //            HttpContext.Session.SetInt32("owner_id", owner_id);
        //            return Redirect("/Account/Owner");

        //        }
                

                
        //    }
        //    else
        //    {
        //        ViewBag.fail = "Sorry. There are no items in that category";
                
        //    }
        //    ViewBag.owners = cat;
        //    ViewBag.sitems = sel_items;
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult Show()
        //{

        //    IEnumerable<ITEM> t1 = _item.GetAllItems();
        //    IEnumerable<USER> t = _user.GetAllUsers();
        //    ViewBag.data1 = t;
        //    ViewBag.data2 = t1;


        //    //var ac = _account.GetAccountByUserId(19);
        //    //ViewBag.acc = ac;

        //    return View();
        //}


        //public IActionResult Show()
        //{
        //    int type_id = 1;
        //    return View();
        //}

    }
}
