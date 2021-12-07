using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class ACCOUNT
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int Balance { get; set; }
        public int User_Id { get; set; }
        [ForeignKey("User_Id")]
        public virtual USER USER { get; set; }
    }

    public class ACCOUNT1
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
    }
    public class ACCOUNT2
    {
        [Key]
        public int Id { get; set; }
        public int Balance { get; set; }
        public int User_Id { get; set; }
        [ForeignKey("User_Id")]
        public virtual USER USER { get; set; }
    }
}
