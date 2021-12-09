using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLPurchasedItemRepository : IPurchasedItemRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLPurchasedItemRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //create
        public void AddPurchasedItem(PURCHASED_ITEM i)
        {
            List<int> maxId;
            if (i.Item_Id % 2 == 0)
            {
                maxId = _sadb.PURCHASED_ITEM.Select(i => i.Id).ToList();
                maxId.Sort();
                i.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 2;

                _sadb.PURCHASED_ITEM.Add(i);
                _sadb.SaveChanges();
            }
            else
            {
                maxId = _apdb.PURCHASED_ITEM.Select(i => i.Id).ToList();
                maxId.Sort();
                i.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 1;

                _apdb.PURCHASED_ITEM.Add(i);
                _apdb.SaveChanges();
            }
        }

        //read
        public IEnumerable<PURCHASED_ITEM> GetAllPurchasedItems()
        {
            List<PURCHASED_ITEM> db1Items = _apdb.PURCHASED_ITEM.ToList();
            List<PURCHASED_ITEM> db2Items = _sadb.PURCHASED_ITEM.ToList();

            IEnumerable<PURCHASED_ITEM> items = db1Items.Concat(db2Items);
            return items;
        }

        public IEnumerable<PURCHASED_ITEM> GetPurchasedItemsByAccId(int accId)
        {
            List<PURCHASED_ITEM> db1Items = _apdb.PURCHASED_ITEM.Where(i => i.Buyer_Account_Id == accId).ToList();
            List<PURCHASED_ITEM> db2Items = _sadb.PURCHASED_ITEM.Where(i => i.Buyer_Account_Id == accId).ToList();

            IEnumerable<PURCHASED_ITEM> items = db1Items.Concat(db2Items);
            return items;
        }

        public PURCHASED_ITEM GetPurchasedItemById(int purchasedItemId)
        {
            PURCHASED_ITEM item;

            if (purchasedItemId % 2 == 0)
            {
                item = _sadb.PURCHASED_ITEM.Where(i => i.Id == purchasedItemId).Single();
            }
            else
            {
                item = _apdb.PURCHASED_ITEM.Where(i => i.Id == purchasedItemId).Single();
            }

            return item;
        }

        public PURCHASED_ITEM GetPurchasedItemByItemId(int itemId)
        {
            PURCHASED_ITEM item;

            if (itemId % 2 == 0)
            {
                item = _sadb.PURCHASED_ITEM.Where(i => i.Item_Id == itemId).Single();
            }
            else
            {
                item = _apdb.PURCHASED_ITEM.Where(i => i.Item_Id == itemId).Single();
            }

            return item;
        }

        //update
        public Boolean UpdatePurchasedItem(PURCHASED_ITEM updatedPurchasedItem)
        {
            PURCHASED_ITEM exist;
            if (updatedPurchasedItem.Item_Id % 2 == 0)
            {
                exist = _sadb.PURCHASED_ITEM.Where(i => i.Id == updatedPurchasedItem.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _sadb.PURCHASED_ITEM.Attach(updatedPurchasedItem);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                exist = _apdb.PURCHASED_ITEM.Where(i => i.Id == updatedPurchasedItem.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _apdb.PURCHASED_ITEM.Attach(updatedPurchasedItem);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public Boolean UpdatePurchasedItem_ItemId(PURCHASED_ITEM oldItem, int newItemId)
        {
            Boolean check = DeletePurchasedItem(oldItem.Item_Id, oldItem.Buyer_Account_Id);
            if (check)
            {
                oldItem.Item_Id = newItemId;
                AddPurchasedItem(oldItem);
                return true;
            }
            return false;
        }

        public Boolean UpdatePurchasedItem_ItemId(int oldItemId, int oldAccId, PURCHASED_ITEM newPurchasedItem)
        {
            Boolean check = DeletePurchasedItem(oldItemId, oldAccId);
            if (check)
            {
                AddPurchasedItem(newPurchasedItem);
                return true;
            }
            return false;
        }


        //delete
        public Boolean DeletePurchasedItem(int itemId, int accId)
        {
            if (itemId % 2 == 0)
            {
                PURCHASED_ITEM i = _sadb.PURCHASED_ITEM.Where(i => i.Buyer_Account_Id == accId && i.Item_Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _sadb.PURCHASED_ITEM.Remove(i);
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                PURCHASED_ITEM i = _apdb.PURCHASED_ITEM.Where(i => i.Buyer_Account_Id == accId && i.Item_Id == itemId).FirstOrDefault();
                if (i != null)
                {
                    _apdb.PURCHASED_ITEM.Remove(i);
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Boolean DeleteAllPromotedItems(int accId)
        {
            List<PURCHASED_ITEM> i = _sadb.PURCHASED_ITEM.Where(i => i.Buyer_Account_Id == accId).ToList();
            if (i.Any())
            {
                _sadb.PURCHASED_ITEM.RemoveRange(i);
                _sadb.SaveChanges();
                return true;
            }

            i = _apdb.PURCHASED_ITEM.Where(i => i.Buyer_Account_Id == accId).ToList();
            if (i.Any())
            {
                _apdb.PURCHASED_ITEM.RemoveRange(i);
                _apdb.SaveChanges();
                return true;
            }
            return false;
        }


    }
}
