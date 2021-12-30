using System;

namespace E_commerce.Data
{
    public class displayed_inventory_item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public int Available_Count { get; set; }
        public int Original_Count { get; set; }
        public string Description { get; set; }
        public DateTime Publish_Date { get; set; }
       
    }
}
