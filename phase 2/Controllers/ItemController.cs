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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;
        private readonly ITypeRepository _typeRepo;
        private readonly IPurchasedItemRepository _purchasedItemRepo;
        private readonly IAccountRepository _accRepo;

        public ItemController(IItemRepository itemRepository, IPurchasedItemRepository purchasedItemRepo, ITypeRepository typeRepo, IAccountRepository accRepo)
        {
            _itemRepo = itemRepository;
            _purchasedItemRepo = purchasedItemRepo;
            _typeRepo = typeRepo;
            _accRepo = accRepo;
        }

        /********************************************* Read *********************************************/
        [HttpGet("{accId}")]
        public ActionResult<string> Dashboard(int accId)
        {
            ACCOUNT acc = _accRepo.GetAccountByAccId(accId);
            if (acc != null)
            {
                string balance = "Balance: " + acc.Balance + "$";
                return balance;
            }

            return NoContent();
        }


        [HttpGet("{Reg_Id}/{item_type}")]
        public ActionResult<IEnumerable<displayed_item>> Item(int Reg_Id, string item_type)
        {
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
                        Sold_Instances = t1.Sold_Instances,
                        Total_Money = t1.Price * t1.Sold_Instances
                    });

                //ViewBag.ItemType = 0;
                return itemList.ToArray();
            }

            else if (item_type == "unsold")
            {
                //unsold items are the items with available count != 0
                IEnumerable<ITEM> myItems = _itemRepo.GetAvailableItemsByAccId(Reg_Id).Where(t => t.Available_Count != 0).ToList();

                itemList = myItems.Join(allTypes, i1 => i1.Type, i2 => i2.Id,
                    (i1, i2) => new displayed_item
                    {
                        Name = i1.Name,
                        Price = i1.Price,
                        Type = i2.Type,
                        Image = i1.Image,
                        Available_Count = i1.Available_Count,
                        Publish_Date = i1.Date
                    });

                //ViewBag.ItemType = 1;
                return itemList.ToArray();
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
                        Purchase_Date = t1.Purchase_Date,
                        Total_Money = t1.Price * t1.Purchased_Count
                    });

                //ViewBag.ItemType = 2;
                return itemList.ToArray();
            }

            return NoContent();
        }



        [HttpGet]
        public ActionResult<IEnumerable<displayed_inventory_item>> Inventory()
        {
            //int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            IEnumerable<ITEM> myItems = _itemRepo.GetAvailableItemsByAccId(1);
            IEnumerable<TYPE> allTypes = _typeRepo.GetAllTypes();
            IEnumerable<displayed_inventory_item> itemList;
            itemList = myItems.Join(allTypes, i1 => i1.Type, i2 => i2.Id,
                    (i1, i2) => new displayed_inventory_item
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
            
            if (type_exist)
            {
                return itemList.ToArray();
            }
            else
            {
                return NoContent();
            }
            //return myItems.ToArray();
            
        }
        /********************************************* ADD ***********************************/
        [HttpPost]
        public void Add(AddItemVM obj)
        {
            ITEM entity = new ITEM
            {
                Name = obj.Name,
                Type = obj.Type,
                Description = obj.Description,
                Price = obj.Price,
                Image = obj.Image,
                Original_Count = obj.Original_Count,
                Available_Count = obj.Available_Count,
            };
            entity.Date = DateTime.Now;
            entity.Owner_Account_Id = 1;
            entity.Status = 1;
            _itemRepo.AddItem(entity);

            //return RedirectToAction("Inventory");
            //return CreatedAtAction("Inventory", new ITEM { Id = obj.Id }, entity);
            //return Redirect("/Item/Inventory");
            //return View();
        }


        /*************************** EDIT ************************************/
        //[HttpPut ("{id}")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id,ITEM entity)
        //{
        //    if (id!=entity.Id)
        //    {
        //        return BadRequest();
        //    }
        //    //int Item_Id = (int)HttpContext.Session.GetInt32("Edit_Item_Id");

        //    //ITEM entity = _itemRepo.GetItemById(id);

        //    //ITEM entity = _itemRepo.GetItemById(1);
        //    //entity.Id = Item_Id;
        //    //entity.Name = obj.Name;
        //    ////entity.Type = obj.Type;
        //    ////entity.Original_Count = obj.Original_Count;
        //    ////int last_av_count = entity.Available_Count;
        //    //int diff = entity.Original_Count - entity.Available_Count;
        //    ////Take the new available count
        //    //entity.Available_Count = obj.Available_Count;
        //    ////Update the original count
        //    //entity.Original_Count = obj.Available_Count + diff;
        //    //entity.Description = obj.Description;
        //    //entity.Price = obj.Price;
        //    //entity.Date = obj.Date;
        //    //entity.Image = obj.Image;
        //    //entity.Owner_Account_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
        //    Boolean x = _itemRepo.UpdateItem(entity);
        //    //Boolean y=_itemRepo.UpdateItemType(entity, obj.Type);
        //    return NoContent();
        //    //return Redirect("/Item/Inventory");
        //    //return View();
        //}

        /*************************** DELETE *****************************************/
        //[HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    //IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(1);
        //    //int Reg_Id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
        //    //IEnumerable<ITEM> t = _itemRepo.GetAvailableItemsByAccId(Reg_Id);
        //    //var tt = t.ToList();
        //    //ViewBag.items = tt;


        //    ITEM entity = new ITEM();
        //    //entity = obj.ITEM;

        //    entity.Id = id;
        //    entity = _itemRepo.GetItemById(entity.Id);
        //    if (entity == null)
        //    {
        //        return NotFound();
        //    }

        //    entity.Status = 0;
        //    Boolean x = _itemRepo.UpdateItem(entity);
        //    return NoContent();
        //    //return Redirect("/Item/Inventory");

        //    //return View();
        //}
    }
}
