using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.View_Models
{
    public class S
    {
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string currentPaasword { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [Required]
        [MinLength(4)]
        [Compare("newPassword")]
        [DataType(DataType.Password)]
        public string reenterNewPassword { get; set; }
        
    }
}
