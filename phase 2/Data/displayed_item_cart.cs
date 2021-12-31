namespace E_commerce.Data
{
    public class displayed_item_cart
    {
        public int item_id { get; set; }
        public int owner_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string image { get; set; }

        public int item_count { get; set; }
    }
}
