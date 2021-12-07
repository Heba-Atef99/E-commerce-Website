using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLItemRepository : IItemRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLItemRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }
        //Create
        public void AddItem(ITEM i)
        {
            int maxId = 0;
            if (i.Type % 2 == 0)
            {
                maxId = _sadb.ITEM.Select(i => i.Id).DefaultIfEmpty().Max();
                i.Id = (maxId > 0) ? maxId + 2 : 2;
                _sadb.ITEM.Add(i);
                _sadb.SaveChanges();
            }
            else
            {
                maxId = _apdb.ITEM.Select(i => i.Id).DefaultIfEmpty().Max();
                i.Id = (maxId > 0) ? maxId + 2 : 1;

                _apdb.ITEM.Add(i);
                _apdb.SaveChanges();

            }
        }

        //Read
        public IEnumerable<ITEM> GetAllItems()
        {
            List<ITEM> db1Items = _apdb.ITEM.ToList();
            List<ITEM> db2Items = _sadb.ITEM.ToList();

            IEnumerable<ITEM> items = db1Items.Concat(db2Items);
            return items;
        }

        public IEnumerable<ITEM> GetItemsByAccId(int id)
        {
            List<ITEM> db1Items = _apdb.ITEM.Where(i => i.Owner_Account_Id == id).ToList();
            List<ITEM> db2Items = _sadb.ITEM.Where(i => i.Owner_Account_Id == id).ToList();

            IEnumerable<ITEM> items = db1Items.Concat(db2Items);
            return items;
        }

        public ITEM GetItemById(int itemId)
        {
            ITEM item;

            if (itemId % 2 == 0)
            {
                item = _sadb.ITEM.Where(i => i.Id == itemId).Single();
            }
            else
            {
                item = _apdb.ITEM.Where(i => i.Id == itemId).Single();
            }

            return item;
        }


        //Update
        public Boolean UpdateItem(ITEM updatedItem)
        {
            ITEM exist;
            if (updatedItem.Type % 2 == 0)
            {
                exist = _sadb.ITEM.Where(i => i.Id == updatedItem.Id).FirstOrDefault();
                if(exist != null)
                {
                    var modified = _sadb.ITEM.Attach(updatedItem);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                exist = _apdb.ITEM.Where(i => i.Id == updatedItem.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _apdb.ITEM.Attach(updatedItem);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        //in case you need to update the name only, send the old item without any modifications
        public Boolean UpdateItemType(ITEM oldItem, int newType)
        {
            Boolean check = DeleteItem(oldItem.Type, oldItem.Owner_Account_Id);
            if(check)
            {
                oldItem.Type = newType;
                AddItem(oldItem);
                return true;
            }
            return false;
        }

        //in case you have other modifications other than the name then send the old item
        //without any modifications and the new item with all the modifications
        public Boolean UpdateItemType(int oldType, int oldOwnerAccId, ITEM newItem)
        {
            Boolean check = DeleteItem(oldType, oldOwnerAccId);
            if (check)
            {
                AddItem(newItem);
                return true;
            }
            return false;
        }

        //Delete
        public Boolean DeleteItem(int typeId, int itemId)
        {
            if (typeId % 2 == 0)
            {
                ITEM i = _sadb.ITEM.Where(i => i.Type == typeId && i.Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _sadb.ITEM.Remove(i);
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                ITEM i = _apdb.ITEM.Where(i => i.Type == typeId && i.Id == itemId).FirstOrDefault();
                if(i != null)
                {
                    _apdb.ITEM.Remove(i);
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Boolean DeleteAllItems(int accId)
        {
            List<ITEM> i = _sadb.ITEM.Where(i => i.Owner_Account_Id == accId).ToList();
            if(i.Any())
            {
                _sadb.ITEM.RemoveRange(i);
                _sadb.SaveChanges();
                return true;
            }

            i = _apdb.ITEM.Where(i => i.Owner_Account_Id == accId).ToList();
            if (i.Any())
            {
                _apdb.ITEM.RemoveRange(i);
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }


    }
}
