using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace E_commerce.ViewModels
{
    public class Search
    {

        public string Name { get; set; }
        public string Sort { get; set; }
=======
namespace E_commerce.Models
{
    public class USER
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }

>>>>>>> original/owner
    }
}
