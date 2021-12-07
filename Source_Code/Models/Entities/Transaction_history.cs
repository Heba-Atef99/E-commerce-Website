using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class TRANSACTION_HISTORY
    {
        [Key]
        public int Id { get; set; }
        public int? Receiver_Id { get; set; }
        [ForeignKey("Receiver_Id")]
        
        public int? Sender_Id { get; set; }
        [ForeignKey("Sender_Id")]
        public virtual ACCOUNT ACCOUNT { get; set; }
        
        public int Money { get; set; }
    }

    public class TRANSACTION1
    {
        [Key]
        public int Id { get; set; }
        public int? Receiver_Id { get; set; }
        [ForeignKey("Receiver_Id")]

        public int Money { get; set; }
    }

    public class TRANSACTION2
    {
        [Key]
        public int Id { get; set; }
        public int? Sender_Id { get; set; }
        [ForeignKey("Sender_Id")]
        public virtual ACCOUNT ACCOUNT { get; set; }
    }

}
