using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class CART
    {
        [Key]
        public int Id { get; set; }
        public int Item_Id { get; set; }
        [ForeignKey ("Item_Id")]
        public virtual ITEM ITEM { get; set; }
        public int Account_Id { get; set; }
        [ForeignKey("Account_Id")]
        public virtual ACCOUNT ACCOUNT { get; set; }
        public int Item_count { get; set; } = 1;
    }
}
