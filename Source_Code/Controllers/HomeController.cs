using E_commerce.Models;
using E_commerce.Models.Repositories;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Http;
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
        private readonly IItemRepository _item;
        public HomeController(IAccountRepository logger, IUserRepository user, IItemRepository item)
        {
            _logger = logger;
            _user = user;
            _item = item;
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
            USER u = new USER
            {
                Name = ob.Name,

                Phone = ob.Phone,
                Address = ob.Address,
            };
            ACCOUNT acc = new ACCOUNT
            {
                Email = ob.Email,
                Pass = BCrypt.Net.BCrypt.HashPassword(ob.Password),
            };
            IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            foreach (var ac in a)
            {
                if(ac.Email == ob.Email) 
                {
                    ViewBag.messageError = "You have already signed up";
                    return View();
                }
                
            }
                
            _user.AddUser(u);
            acc.User_Id = u.Id;
            _logger.AddAccount(acc);
            HttpContext.Session.SetInt32("User_Reg_Id",acc.Id);
            
            return Redirect("/Category/Main");

        }
        public IActionResult Homepage()
        {
            ViewBag.l1 =  _item.GetAllItems();
            ViewBag.l2 = _logger.GetAllAccountEmailsAndPass();
            return View();
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
                    if(BCrypt.Net.BCrypt.Verify(objc.Password, acc.Pass))
                    {
                        HttpContext.Session.SetInt32("User_Reg_Id", acc.Id);
                        return Redirect("/Category/Main");
                    }
                    else 
                    {
                        ViewBag.messageError = "wrong password";
                        return View();
                    }
                   
                }
            }
            ViewBag.messageError = "You need to sign up first";
            return View();
        }
        public IActionResult welcome()
        {
            return View();
        }
       
            [HttpPost]
        public IActionResult Logout(logoutvm L)
        {
            if (L != null) HttpContext.Session.SetInt32("User_Reg_Id", 0);
            return Redirect("/Home/index");
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
