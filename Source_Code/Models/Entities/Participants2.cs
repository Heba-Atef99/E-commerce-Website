using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Participants2
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

    }
}
