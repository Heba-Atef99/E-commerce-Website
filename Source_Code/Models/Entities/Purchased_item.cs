using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace E_commerce.Models

{
    public class PURCHASED_ITEM
    {
        [Key]
        public int Id { get; set; }
        public int? Item_Id { get; set; }
        [ForeignKey("Item_Id")]
        public virtual ITEM ITEM { get; set; }

        public int? Promoted_Account_Id { get; set; }
        [ForeignKey("Promoted_Account_Id")]
        public virtual ACCOUNT ACCOUNT { get; set; }

        public int Purchased_Count { get; set; }
        public DateTime Date { get; set; }

    }
}
