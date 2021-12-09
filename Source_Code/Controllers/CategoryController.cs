using Microsoft.AspNetCore.Mvc;
using E_commerce.Models.Repositories;
using E_commerce.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ITypeRepository _typeRepo;

        public CategoryController(ITypeRepository typeRepository)
        {
            _typeRepo = typeRepository;
        }
        public IActionResult Main(int type)
        {
            IEnumerable<TYPE> t = _typeRepo.GetAllTypes();
            ViewBag.types=t;
            
            if(type != 0)
            {
                
                HttpContext.Session.SetInt32("Type_Id", type);
                return Redirect("/Category/CatItems");
            }
            
            return View();
        }
        public IActionResult CatItems()
        {
            return View();
        }
    }
}
