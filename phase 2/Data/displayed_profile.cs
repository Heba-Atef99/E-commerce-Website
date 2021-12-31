using System;

namespace E_commerce.Data
{
    public class displayed_profile
    {
        public int Acc_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; } = 0;
        public string Address { get; set; }
        public int Phone { get; set; }
        public string password { get; set; }
    }
}
