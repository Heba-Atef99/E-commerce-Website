using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using E_commerce.Models.Repositories;
using E_commerce.Models;

namespace E_commerce.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IItemRepository _itemRepo;

        public OwnerController(IItemRepository itemRepository)
        {
            _itemRepo = itemRepository;
        }
        public IActionResult Owner()
        {
            IEnumerable<ITEM> t = _itemRepo.GetItemsByAccId(1);
            return View(t);
        }
    }
}
