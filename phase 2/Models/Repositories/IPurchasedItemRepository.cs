using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IPurchasedItemRepository
    {
        public void AddPurchasedItem(PURCHASED_ITEM i);
        public IEnumerable<PURCHASED_ITEM> GetAllPurchasedItems();
        public IEnumerable<PURCHASED_ITEM> GetPurchasedItemsByAccId(int accId);
        public PURCHASED_ITEM GetPurchasedItemById(int purchasedItemId);
        public PURCHASED_ITEM GetPurchasedItemByItemId(int itemId);
        public Boolean UpdatePurchasedItem(PURCHASED_ITEM updatedPurchasedItem);
        public Boolean UpdatePurchasedItem_ItemId(PURCHASED_ITEM oldItem, int newItemId);
        public Boolean UpdatePurchasedItem_ItemId(int oldItemId, int oldAccId, PURCHASED_ITEM newPurchasedItem);
        public Boolean DeletePurchasedItem(int itemId, int accId);
        public Boolean DeleteAllPromotedItems(int accId);
    }
}
