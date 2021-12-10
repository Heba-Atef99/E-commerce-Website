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
    public class DashboardController : Controller
    {
        private readonly IItemRepository _itemRepo;

        public DashboardController(IItemRepository itemRepository)
        {
            _itemRepo = itemRepository;
        }
        public IActionResult Dashboard(string btn)
        {
            if(btn != null)
            {
                HttpContext.Session.SetString("Item_Type", btn);
                return Redirect("/Dashboard/Item");
            }
            return View();
        }
        public IActionResult Item()
        {
            string item_type = HttpContext.Session.GetString("Item_Type");
            if (item_type == "sold")
            {
            }
            else if (item_type == "unsold")
            {
            }
            else if (item_type == "purchased")
            {
            }
            else
            {
                //return Redirect("/Dashboard/Dashboard");
            }
            ITEM tt = new ITEM {Id = 3, Name = "Batata", Type = 3, Owner_Account_Id = 1, Available_Count=5, 
                Description = "Sweet Batata", Original_Count= 10, Price = 50, Date = DateTime.Now, Image="lnlnl"};
            //_itemRepo.AddItem(tt);
            //_itemRepo.UpdateItemType(tt, 2);
            //Boolean check = _itemRepo.DeleteItem(2, 2);
            IEnumerable<ITEM> t = _itemRepo.GetAllItems();
            //IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(2);

            return View(t);
        }
    }
}
