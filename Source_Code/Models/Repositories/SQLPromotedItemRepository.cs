using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLPromotedItemRepository : IPromotedItemRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLPromotedItemRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //Create
        public void AddPromotedItem(PROMOTED_ITEM i)
        {
            List<int> maxId;
            if (i.Item_Id % 2 == 0)
            {
                maxId = _sadb.PROMOTED_ITEM.Select(i => i.Id).ToList();
                maxId.Sort();
                i.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 2;

                _sadb.PROMOTED_ITEM.Add(i);
                _sadb.SaveChanges();
            }
            else
            {
                maxId = _apdb.PROMOTED_ITEM.Select(i => i.Id).ToList();
                maxId.Sort();
                i.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 2;

                _apdb.PROMOTED_ITEM.Add(i);
                _apdb.SaveChanges();
            }
        }

        //Read
        public IEnumerable<PROMOTED_ITEM> GetAllPromotedItems()
        {
            List<PROMOTED_ITEM> db1Items = _apdb.PROMOTED_ITEM.ToList();
            List<PROMOTED_ITEM> db2Items = _sadb.PROMOTED_ITEM.ToList();

            IEnumerable<PROMOTED_ITEM> items = db1Items.Concat(db2Items);
            return items;
        }

        public IEnumerable<PROMOTED_ITEM> GetPromotedItemsByAccId(int accId)
        {
            List<PROMOTED_ITEM> db1Items = _apdb.PROMOTED_ITEM.Where(i => i.Promoted_Account_Id == accId).ToList();
            List<PROMOTED_ITEM> db2Items = _sadb.PROMOTED_ITEM.Where(i => i.Promoted_Account_Id == accId).ToList();

            IEnumerable<PROMOTED_ITEM> items = db1Items.Concat(db2Items);
            return items;
        }

        public PROMOTED_ITEM GetPromotedItemById(int promotedItemId)
        {
            PROMOTED_ITEM item;

            if (promotedItemId % 2 == 0)
            {
                item = _sadb.PROMOTED_ITEM.Where(i => i.Id == promotedItemId).Single();
            }
            else
            {
                item = _apdb.PROMOTED_ITEM.Where(i => i.Id == promotedItemId).Single();
            }

            return item;
        }

        //Update
        public Boolean UpdatePromotedItem(PROMOTED_ITEM updatedPromotedItem)
        {
            PROMOTED_ITEM exist;
            if (updatedPromotedItem.Item_Id % 2 == 0)
            {
                exist = _sadb.PROMOTED_ITEM.Where(i => i.Id == updatedPromotedItem.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _sadb.PROMOTED_ITEM.Attach(updatedPromotedItem);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                exist = _apdb.PROMOTED_ITEM.Where(i => i.Id == updatedPromotedItem.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _apdb.PROMOTED_ITEM.Attach(updatedPromotedItem);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public Boolean UpdatePromotedItem_ItemId(PROMOTED_ITEM oldItem, int newItemId)
        {
            Boolean check = DeletePromotedItem(oldItem.Item_Id, oldItem.Promoted_Account_Id);
            if (check)
            {
                oldItem.Item_Id = newItemId;
                AddPromotedItem(oldItem);
                return true;
            }
            return false;
        }

        public Boolean UpdatePromotedItem_ItemId(int oldItemId, int oldAccId, PROMOTED_ITEM newPromotedItem)
        {
            Boolean check = DeletePromotedItem(oldItemId, oldAccId);
            if (check)
            {
                AddPromotedItem(newPromotedItem);
                return true;
            }
            return false;
        }

        //Delete
        public Boolean DeletePromotedItem(int itemId, int accId)
        {
            if (itemId % 2 == 0)
            {
                PROMOTED_ITEM i = _sadb.PROMOTED_ITEM.Where(i => i.Promoted_Account_Id == accId && i.Item_Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _sadb.PROMOTED_ITEM.Remove(i);
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                PROMOTED_ITEM i = _apdb.PROMOTED_ITEM.Where(i => i.Promoted_Account_Id == accId && i.Item_Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _apdb.PROMOTED_ITEM.Remove(i);
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Boolean DeleteAllPromotedItems(int accId)
        {
            List<PROMOTED_ITEM> i = _sadb.PROMOTED_ITEM.Where(i => i.Promoted_Account_Id == accId).ToList();
            if (i.Any())
            {
                _sadb.PROMOTED_ITEM.RemoveRange(i);
                _sadb.SaveChanges();
                return true;
            }

            i = _apdb.PROMOTED_ITEM.Where(i => i.Promoted_Account_Id == accId).ToList();
            if (i.Any())
            {
                _apdb.PROMOTED_ITEM.RemoveRange(i);
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
