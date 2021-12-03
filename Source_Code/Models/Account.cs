using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int Balance { get; set; }
        public int? User_id { get; set; }
        [ForeignKey("User_id")]
        public virtual User User { get; set; }


    }
}
