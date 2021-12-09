using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using E_commerce.Models.Repositories;
using E_commerce.Models;
using E_commerce.ViewModels.ItemVM;

namespace E_commerce.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepo;

        public ItemController(IItemRepository itemRepository)
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
            _itemRepo.AddItem(tt);

            //_itemRepo.UpdateItemType(tt, 2);
            //Boolean check = _itemRepo.DeleteItem(2, 2);
            IEnumerable<ITEM> t = _itemRepo.GetAllItems();
            //IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(2);

            return View(t);
        }
        public IActionResult Inventory()
        {
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(Reg_Id);
            var type_exist=t.Any();
            string No_Items="";
            if (type_exist)
            {
                
            }
            else
            {
                No_Items = "You have no items yet";
            }
            ViewBag.list = t;
            ViewBag.No_Items = No_Items;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inventory(EditItemVM obj)
        {
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(Reg_Id);
            ViewBag.list = t;
            ITEM entity = new ITEM();
            //entity = obj.ITEM;

            entity.Id = obj.Id;
            //entity = _itemRepo.GetItemById(entity.Id);
            
            
                HttpContext.Session.SetInt32("Edit_Item_Id", (int)obj.Id);
            return Redirect("/Item/Edit");

            //return View("/Item/Edit");
            //return View();
        }
        public IActionResult Add()
        {
            ITEM t= new ITEM();
            return View(t);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddItemVM obj)
        {
            ITEM entity=new ITEM();

            entity.Name = obj.Name;
            entity.Type = obj.Type;
            entity.Original_Count = obj.Original_Count;
            entity.Description = obj.Description;
            entity.Price = obj.Price;
            entity.Date = DateTime.Now;
            entity.Image = obj.Image;
            entity.Owner_Account_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");

            _itemRepo.AddItem(entity);
            return Redirect("/Item/Inventory");
            //return View();
        }


        public IActionResult Which_Item_Edit()
        {
            return View();
        }
        public IActionResult Edit()
        {
            int Item_Id = (int)HttpContext.Session.GetInt32("Edit_Item_Id");
            ITEM t = _itemRepo.GetItemById(Item_Id);
            ViewBag.Item = t;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditItemVM obj)
        {
            int Item_Id = (int)HttpContext.Session.GetInt32("Edit_Item_Id");
            ITEM entity = _itemRepo.GetItemById(Item_Id);
            ViewBag.Item= entity;
            //ITEM entity = _itemRepo.GetItemById(1);
            entity.Id = Item_Id;
            entity.Name = obj.Name;
            //entity.Type = obj.Type;
            entity.Original_Count = obj.Original_Count;
            entity.Available_Count = obj.Available_Count;
            entity.Description = obj.Description;
            entity.Price = obj.Price;
            entity.Date = obj.Date;
            entity.Image = obj.Image;
            entity.Owner_Account_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            Boolean x = _itemRepo.UpdateItem(entity);
            //Boolean y=_itemRepo.UpdateItemType(entity, obj.Type);

            return Redirect("/Item/Inventory");
            //return View();
        }

        public IActionResult Delete()
        {
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(Reg_Id);
            var tt = t.ToList();
            ViewBag.items=tt;
            return View();


            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete( DeleteItemVM obj)
        {
            //IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(1);
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(Reg_Id);
            var tt = t.ToList();
            ViewBag.items = tt;

            ITEM entity = new ITEM();
            //entity = obj.ITEM;
            
            entity.Id=obj.Id;
            entity = _itemRepo.GetItemById(entity.Id);
            
            _itemRepo.DeleteItem( entity.Type,entity.Id);


            return Redirect("/Item/Inventory");

            //return View();
        }
    }
}
