using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IItemRepository
    {
        public ITEM GetItemById(int itemId);
        IEnumerable<ITEM> GetAllItems(); 
        IEnumerable<ITEM> GetItemsByAccId(int id); 
        Boolean UpdateItem(ITEM itemChanges);
        Boolean UpdateItemType(ITEM item, int newType);
        Boolean UpdateItemType(ITEM oldItem, ITEM newItem, int newType);
        Boolean DeleteItem(int typeId, int accId); 
        Boolean RemoveAllItems(int accId);
        void AddItem(ITEM i); 

    }
}
