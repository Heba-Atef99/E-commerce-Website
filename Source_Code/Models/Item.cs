using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }
        public DateTime Date { get; set; }
        public int Original_count { get; set; }
        public int Available_count { get; set; }
        public int? Owner_account_id { get; set; }
        [ForeignKey("Owner_account_id")]
        public virtual Account Account { get; set; }

    }
}
