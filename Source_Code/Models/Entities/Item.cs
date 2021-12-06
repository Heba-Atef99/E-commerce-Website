using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class ITEM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int Original_Count { get; set; }
        public int Available_Count { get; set; }
        public int Owner_Account_Id { get; set; }
        [ForeignKey("Owner_Account_Id")]
        public virtual ACCOUNT ACCOUNT { get; set; }

    }
}
