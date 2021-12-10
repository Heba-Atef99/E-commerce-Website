using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.ViewModels
{
    public class Signup
    {
        [Required]

        public string Name { get; set; }
        [Required]

       // [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(4)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
