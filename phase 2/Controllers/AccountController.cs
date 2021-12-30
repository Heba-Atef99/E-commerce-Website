using System;
using System.Collections.Generic;
using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _logger;
        private readonly IUserRepository _user;
        private readonly IItemRepository _item;
        public AccountController(IAccountRepository logger, IUserRepository user, IItemRepository item)
        {
            _logger = logger;
            _user = user;
            _item = item;
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ITEM>> Owner(int id)
        {
            return _item.GetItemsByAccId(id).ToArray();

        }
        [HttpGet("{id}")]
        public ActionResult<int> Deposit(int id)
        {
            return _logger.GetAccountByAccId(id).Balance;

        }
        [HttpPost]
        public void Deposit(ACCOUNT ob)
        {
            int id = ob.Id;
            //addbalance = ob;
            ACCOUNT account = _logger.GetAccountByAccId(id);
            //account.Balance = account.Balance + ob.Balance;
            account.Balance = account.Balance + ob.Balance;
            bool k = _logger.UpdateAccount(account, 0);
            //return CreatedAtAction("Deposit", new ACCOUNT { Id = ob.Id }, ob);
            //return RedirectToAction("Deposit");
            //return RedirectToActionResult("Deposit",id);
        }

    }
}
