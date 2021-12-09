using System;

namespace E_commerce.Data
{
    public class displayed_item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public int Available_Count { get; set; }
        public int Sold_Instances { get; set; }
        public int Purchased_Count { get; set; }
        public DateTime Publish_Date { get; set; }
        public DateTime Purchase_Date { get; set; }
    }
}