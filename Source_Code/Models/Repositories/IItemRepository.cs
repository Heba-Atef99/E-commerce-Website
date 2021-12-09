using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IItemRepository
    {
        ITEM GetItemById(int itemId);
        IEnumerable<ITEM> GetAllItems(); 
        IEnumerable<ITEM> GetItemsByAccId(int id);
        IEnumerable<ITEM> GetAvailableItemsByAccId(int id);
        Boolean UpdateItem(ITEM updatedItem);
        Boolean UpdateItemType(ITEM oldItem, int newType);
        Boolean UpdateItemType(int oldType, int oldOwnerAccId, ITEM newItem);
        Boolean DeleteItem(int typeId, int accId); 
        Boolean DeleteAllItems(int accId);
        void AddItem(ITEM i); 

    }
}
