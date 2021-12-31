using E_commerce.Data;
using System.Collections.Generic;
namespace E_commerce.Data
{
    public class displayed_cart
    {
        public List<displayed_item_cart> items { get; set; }
        public int total_price { get; set; }
        public int balance { get; set; }
    }
}
