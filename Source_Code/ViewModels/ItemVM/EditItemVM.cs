using E_commerce.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.ViewModels.ItemVM
{
    public class EditItemVM
    {
        
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int Original_Count { get; set; }
        public int Available_Count { get; set; }
        public int Owner_Account_Id { get; set; }
        

    }
}
