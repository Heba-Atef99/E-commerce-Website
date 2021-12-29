using E_commerce.Data;
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
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IAccountRepository _logger;
        private readonly IUserRepository _user;
        private readonly IItemRepository _item;
        public WeatherForecastController(IAccountRepository logger, IUserRepository user, IItemRepository item)
        {
            _logger = logger;
            _user = user;
            _item = item;
        }
        [HttpGet]
        public ActionResult<IEnumerable<USER>> signin()
        {
             ;
            return _user.GetAllUsers().ToArray();
        }

        [HttpGet("{id}")]

        public ActionResult<ACCOUNT> Getaccount(int id)
        {
            ACCOUNT commandItem = _logger.GetAccountByAccId(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            return commandItem;
        }
        //POST:     api/commands
        [HttpPost]
        public void signup(Signup ob)
        {
            USER u = new USER
            {
                Name = ob.Name,

                Phone = ob.Phone,
                Address = ob.Address,
            };
        
        ACCOUNT acc = new ACCOUNT
        {
            Email = ob.Email,
            Pass = ob.Pass,
        };
        _user.AddUser(u);
            acc.User_Id = u.Id;
            _logger.AddAccount(acc);

           // return RedirectToAction("signin");
        }
    }
}
