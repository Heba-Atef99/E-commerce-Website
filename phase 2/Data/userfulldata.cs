using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Data
{
    public class userfulldata
    {
        public string Name { get; set; }
        public string Email { get; set; }
       
       
        public int Phone { get; set; }
        public int Balance { get; set; } = 0;
        public string Address { get; set; }
    }
}
