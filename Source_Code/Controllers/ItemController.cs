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
using E_commerce.Data;

namespace E_commerce.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepo;
        private readonly ITypeRepository _typeRepo;
        private readonly IPurchasedItemRepository _purchasedItemRepo;

        public ItemController(IItemRepository itemRepository, IPurchasedItemRepository purchasedItemRepo, ITypeRepository typeRepo)
        {
            _itemRepo = itemRepository;
            _purchasedItemRepo = purchasedItemRepo;
            _typeRepo = typeRepo;
        }

        public IActionResult Dashboard(string btn)
        {
            if(btn != null)
            {
                HttpContext.Session.SetString("Item_Type", btn);
                return Redirect("/Item/Item");
            }
            return View();
        }
        public IActionResult Item()
        {
            string item_type = HttpContext.Session.GetString("Item_Type");
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<TYPE> allTypes = _typeRepo.GetAllTypes();
            IEnumerable<PURCHASED_ITEM> myPurchased = _purchasedItemRepo.GetPurchasedItemsByAccId(Reg_Id);
            IEnumerable<displayed_item> itemList;
            if (item_type == "sold")
            {
                IEnumerable<ITEM> myItems = _itemRepo.GetItemsByAccId(Reg_Id);
                IEnumerable<PURCHASED_ITEM> allPurchasedItems = _purchasedItemRepo.GetAllPurchasedItems().
                    GroupBy(item => item.Item_Id).
                    Select(g => g.First()).
                    ToList();

                itemList = allPurchasedItems.Join(myItems, i1 => i1.Item_Id, i2 => i2.Id,
                    (i1, i2) => new
                    { 
                        Name = i2.Name,
                        Price = i2.Price,
                        Type = i2.Type,
                        Image = i2.Image,
                        Publish_Date = i2.Date,
                        Sold_Instances = i2.Original_Count - i2.Available_Count
                    }).Join(allTypes, t1 => t1.Type, t2 => t2.Id,
                    (t1, t2) => new displayed_item
                    {
                        Name = t1.Name,
                        Price = t1.Price,
                        Type = t2.Type,
                        Image = t1.Image,
                        Publish_Date = t1.Publish_Date,
                        Sold_Instances = t1.Sold_Instances
                    });

                ViewBag.ItemType = 0;
            }

            else if (item_type == "unsold")
            {
                //unsold items are the items with available count != 0
                IEnumerable<ITEM> myItems = _itemRepo.GetAvailableItemsByAccId(Reg_Id).Where(t => t.Available_Count != 0).ToList();

                itemList = myItems.Join(allTypes, i1 => i1.Type, i2 => i2.Id, 
                    (i1, i2) => new displayed_item {
                        Name = i1.Name,
                        Price = i1.Price,
                        Type = i2.Type,
                        Image = i1.Image,
                        Available_Count = i1.Available_Count,
                        Publish_Date = i1.Date
                    });
                
                ViewBag.ItemType = 1;
            }

            else if (item_type == "purchased")
            {
                IEnumerable<ITEM> allItems = _itemRepo.GetAllItems();

                itemList = myPurchased.Join(allItems, i1 => i1.Item_Id, i2 => i2.Id,
                    (i1, i2) => new 
                    {
                        Name = i2.Name,
                        Price = i2.Price,
                        Type = i2.Type,
                        Image = i2.Image,
                        Purchased_Count = i1.Purchased_Count,
                        Purchase_Date = i2.Date
                    }).Join(allTypes, t1 => t1.Type, t2 => t2.Id,
                    (t1, t2) => new displayed_item
                    {
                        Name = t1.Name,
                        Price = t1.Price,
                        Type = t2.Type,
                        Image = t1.Image,
                        Purchased_Count = t1.Purchased_Count,
                        Purchase_Date = t1.Purchase_Date
                    });

                ViewBag.ItemType = 2;
            }
            else
            {
                return Redirect("/Item/Dashboard");
            }
            return View(itemList);

        }
        
        public IActionResult Inventory()
        {
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> myItems = _itemRepo.GetAvailableItemsByAccId(Reg_Id);
            IEnumerable<TYPE> allTypes = _typeRepo.GetAllTypes();
            IEnumerable<displayed_item> itemList;
            itemList = myItems.Join(allTypes, i1 => i1.Type, i2 => i2.Id,
                    (i1, i2) => new displayed_item
                    {
                        Name = i1.Name,
                        Price = i1.Price,
                        Type = i2.Type,
                        Image = i1.Image,
                        Available_Count = i1.Available_Count,
                        Original_Count=i1.Original_Count,
                        Description=i1.Description,
                        Publish_Date = i1.Date
                    });
            var type_exist= myItems.Any();
            string No_Items="";
            if (type_exist)
            {
                
            }
            else
            {
                No_Items = "You have no items yet";
            }
            ViewBag.list = itemList;
            ViewBag.items = myItems;
            ViewBag.No_Items = No_Items;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inventory(EditItemVM obj)
        {
            int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> myItems = _itemRepo.GetAvailableItemsByAccId(Reg_Id);
            IEnumerable<TYPE> allTypes = _typeRepo.GetAllTypes();
            IEnumerable<displayed_item> itemList;
            itemList = myItems.Join(allTypes, i1 => i1.Type, i2 => i2.Id,
                    (i1, i2) => new displayed_item
                    {
                        Name = i1.Name,
                        Price = i1.Price,
                        Type = i2.Type,
                        Image = i1.Image,
                        Available_Count = i1.Available_Count,
                        Original_Count = i1.Original_Count,
                        Description = i1.Description,
                        Publish_Date = i1.Date
                    });
            var type_exist = myItems.Any();
            string No_Items = "";
            if (type_exist)
            {

            }
            else
            {
                No_Items = "You have no items yet";
            }
            ViewBag.list = itemList;
            ViewBag.items = myItems;
            ViewBag.No_Items = No_Items;
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
            IEnumerable<TYPE> allTypes = _typeRepo.GetAllTypes();
            ViewBag.types=allTypes;
            return View(t);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddItemVM obj)
        {
            ITEM entity=new ITEM();

            entity.Name = obj.Name;
            entity.Type = obj.Type;
            entity.Available_Count = obj.Available_Count;
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
            ViewBag.Item = entity;
            //ITEM entity = _itemRepo.GetItemById(1);
            entity.Id = Item_Id;
            entity.Name = obj.Name;
            //entity.Type = obj.Type;
            //entity.Original_Count = obj.Original_Count;
            //int last_av_count = entity.Available_Count;
            int diff = entity.Original_Count - entity.Available_Count;
            //Take the new available count
            entity.Available_Count = obj.Available_Count;
            //Update the original count
            entity.Original_Count = obj.Available_Count + diff;
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
