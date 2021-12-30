using E_commerce.Data;
using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class HomeController : ControllerBase
    {
        private readonly IAccountRepository _logger;
        private readonly IUserRepository _user;
        private readonly IItemRepository _item;
        public HomeController(IAccountRepository logger, IUserRepository user, IItemRepository item)
        {
            _logger = logger;
            _user = user;
            _item = item;
        }
        [HttpGet]
        public ActionResult<IEnumerable<USER>> signin()
        {
            //_user.DeleteUser(41);

            return _user.GetAllUsers().ToArray();
        }

        [HttpGet("{id}")]

        public ActionResult<userfulldata> Getaccount(int id)
        {
            ACCOUNT account = _logger.GetAccountByAccId(id);
            int x = account.User_Id;
            USER u = _user.GetUserById(x);
            userfulldata s = new userfulldata
            {
                Name = u.Name,
                Phone = u.Phone,
                Address = u.Address,
                Email = account.Email,
                Balance = account.Balance


            };

            return s;


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
                Pass = BCrypt.Net.BCrypt.HashPassword(ob.Pass),
            };
            IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            foreach (var ac in a)
            {
                if (ac.Email == ob.Email)
                {
                    throw new ApplicationException("you have an account already !!!!!");
                }

            }

            _user.AddUser(u);
            acc.User_Id = u.Id;
            _logger.AddAccount(acc);

            // return RedirectToAction("signin");
        }
        //POST:     api/commands
        [HttpPost]
        public ActionResult<ACCOUNT> login(Signup objc)
        {
            ACCOUNT ac = new ACCOUNT
            {
                Email = objc.Email,
                Pass = objc.Pass,
            };
            IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            foreach (var acc in a)
            {
                if (acc.Email == objc.Email)
                {
                    if (BCrypt.Net.BCrypt.Verify(objc.Pass, acc.Pass))
                    {

                        return RedirectToAction("Getaccount", new { acc.Id });
                    }
                    else
                    {
                        throw new ApplicationException("wrong password");


                    }

                }
            }
            throw new ApplicationException("You don't have an acoount please sign up first ");

        }
    }
}
