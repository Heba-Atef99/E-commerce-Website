using E_commerce.Models;
using E_commerce.Models.Repositories;
using E_commerce.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountRepository _logger;
        private readonly IUserRepository _user;
        public HomeController(IAccountRepository logger, IUserRepository user)
        {
            _logger = logger;
            _user = user;
        }





        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Signup ob)
        {
            ACCOUNT acc = new ACCOUNT
            {
                Email = ob.Email,
                Pass = ob.Password,
            };
            IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            foreach (var ac in a)
            {
                if(ac.Email == ob.Email) 
                {
                    ViewBag.EmailExistError = "You have already signed up";
                    return Redirect("/Home/index");
                }
                
            }

                 _logger.AddAccount(acc);
            USER u = new USER
            {
                Name = ob.Name,

                Phone = ob.Phone,
                Address = ob.Address,
            };
            _user.AddUser(u);

            return Redirect("/Home/homepage");

        }
        public IActionResult Homepage()
        {
            IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            return View(a);
        }
        ///login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Signup objc)
        {
            ACCOUNT ac = new ACCOUNT
            {
                Email = objc.Email,
                Pass = objc.Password,
            };
            IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            foreach (var acc in a)
            {
                if (acc.Email == objc.Email)
                {
                    if(acc.Pass==objc.Password)
                    {
                        return Redirect("/Home/welcome");
                    }
                    else 
                    {
                        ViewBag.PasswordError = "wrong password";
                        return View();
                    }
                   
                }
                else 
                {
                    ViewBag.EmailExistError = "You need to signed up first";
                }

            }
            return View();
        }
        public IActionResult welcome()
        {
            return View();
        }
    }
}
