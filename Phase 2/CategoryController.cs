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
    [Route("[controller]/[Action]")]
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

        //get all items
        //                 /controller
        [HttpGet]
        public ActionResult<IEnumerable<ITEM>> GetItems()
        {
            return _item.GetAllItems().ToArray();
        }

        // get items in a specific category
        //                /controller/type_id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ITEM>> GetCategoryItems(int id)
        {
            var items = _item.GetAllItems().ToArray();
            var type_exist = items.ToList().Any(u => u.Type == id);
            var sel_items = items.Where(u => u.Type == id && u.Status == 1).OrderBy(i => i.Name).ToArray();
            if (type_exist)
            {
                return sel_items;
            }
            else
            {
                return NotFound();
            }
        }
        // search category items by name
        [HttpGet("{id}/{name}")]
        public ActionResult<IEnumerable<ITEM>> Search(int id, string inst, string name)
        {
            var items = _item.GetAllItems().ToArray();
            var type_exist = items.ToList().Any(u => u.Type == id);
            var sel_items = items.Where(u => u.Type == id && u.Status == 1).OrderBy(i => i.Name).ToArray();
            if (type_exist)
            {
                var name_exist = items.ToList().Any(u => u.Name == name);
                if (name_exist)
                {
                    sel_items = sel_items.Where(i => i.Name.Contains(name)).ToArray();
                    return sel_items;
                }

                else { return NotFound(); }
            }
            else { return NotFound(); }

        }
        // sort category items by price
        [HttpGet("{id}/{order}")]
        public ActionResult<IEnumerable<ITEM>> SortPrice(int id, string inst, string order)
        {
            var items = _item.GetAllItems().ToArray();
            var type_exist = items.ToList().Any(u => u.Type == id);
            var sel_items = items.Where(u => u.Type == id && u.Status == 1).OrderBy(i => i.Name).ToArray();
            if (type_exist)
            {
                if (order == "DSC")
                {
                    sel_items = sel_items.OrderByDescending(i => i.Price).ToArray();
                }
                else if (order == "ASC")
                {
                    sel_items = sel_items.OrderBy(i => i.Price).ToArray();
                }
                return sel_items;
            }
            else { return NotFound(); }

        }

        // to add to cart  
        [HttpPost]
        public void Add_To_Cart(CART ob)
        {
            _cart.AddToCart(ob);
        }
        //show cart items for specific account
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CART>> Add_To_Cart(int id)
        {
            return _cart.GetCartByAccId(id).ToArray();
        }


    }
}
