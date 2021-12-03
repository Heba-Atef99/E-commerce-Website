using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Promoted_item
    {
        [Key]
        public int Id { get; set; }
        public int? Item_id { get; set; }
        [ForeignKey("Item_id")]
        public virtual Item Item { get; set; }

        public int? User_promoted_account_id { get; set; }
        [ForeignKey("User_promoted_account_id")]
        public virtual Account Account { get; set; }

    }
}
