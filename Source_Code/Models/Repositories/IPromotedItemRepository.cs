using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IPromotedItemRepository
    {
        public void AddPromotedItem(PROMOTED_ITEM i);
        public IEnumerable<PROMOTED_ITEM> GetAllPromotedItems();
        public IEnumerable<PROMOTED_ITEM> GetPromotedItemsByAccId(int accId);
        public PROMOTED_ITEM GetPromotedItemById(int promotedItemId);
        public Boolean UpdateItem(PROMOTED_ITEM updatedPromotedItem);
        public Boolean UpdatePromotedItem_ItemId(PROMOTED_ITEM oldItem, int newItemId);
        public Boolean UpdatePromotedItem_ItemId(int oldItemId, int oldAccId, PROMOTED_ITEM newPromotedItem);
        public Boolean DeletePromotedItem(int itemId, int accId);
        public Boolean DeleteAllPromotedItems(int accId);
    }
}
