using E_commerce.Models;
using E_Commerce.View_Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
   
    public class myAccountController : Controller
    {
        private readonly IAccountRepository _AccountRepository;

        private readonly IUserRepository _UserRepository;
        public myAccountController(IAccountRepository AccountRepository, IUserRepository UserRepository)
        {
            _AccountRepository = AccountRepository;
           _UserRepository= UserRepository;

        }

        public IActionResult Index()
        {
            int account_id=(int) HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a =_AccountRepository.GetAccountByAccId(account_id);
            USER b =_UserRepository.GetUserById(a.User_Id);

            var mytuple=Tuple.Create(a,b);

            return View(mytuple);
        }
        public IActionResult changeName()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult changeName(USER obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER entity =_UserRepository.GetUserById(a.User_Id);
            entity.Id = a.User_Id;
            entity.Name = obj.Name;
            entity.Address = obj.Address;
            entity.Phone = obj.Phone;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

           _UserRepository.UpdateUserName(a.User_Id, entity);

            return RedirectToAction("Index");

        }
        public IActionResult changePassword()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult changePassword(S obj)
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
           
            if (BCrypt.Net.BCrypt.Verify(obj.currentPaasword, a.Pass))
            {
               if(obj.newPassword== obj.reenterNewPassword)
				{
                    a.Pass = BCrypt.Net.BCrypt.HashPassword(obj.newPassword);
                    ACCOUNT entity = _AccountRepository.GetAccountByAccId(a.Id);
                    entity.Id = a.Id;
                    entity.Email = a.Email;
                    entity.Pass = a.Pass;
                    entity.Balance = a.Balance;
                    entity.User_Id = a.User_Id;
                   
                    _AccountRepository.UpdateAccount(entity, 1);


                }
               else
				{
                    ViewBag.messageError = "Passwords don't match";
                    return View();
                }
            }
            else
            {
                ViewBag.messageError = "wrong password";
                return View();
            }

            return RedirectToAction("Index");

        }
        public IActionResult changeEmail()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult changeEmail(ACCOUNT obj)
        {

            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT entity = _AccountRepository.GetAccountByAccId(account_id);
            entity.Id = account_id;
            entity.Email = obj.Email;
            entity.Pass = obj.Pass;
            entity.Balance = obj.Balance;
            entity.User_Id = obj.User_Id;
            //entity.USER =_UserRepository.GetUserById(entity.User_Id);
             _AccountRepository.UpdateAccount(entity, 1);

           
            return RedirectToAction("Index");

        }
        public IActionResult changeAddress()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult changeAddress(USER obj)
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER entity =_UserRepository.GetUserById(a.User_Id);
            entity.Id = a.User_Id;
            entity.Name = obj.Name;
            entity.Address= obj.Address;
            entity.Phone= obj.Phone;
           
            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

           _UserRepository.UpdateUser(entity);

            return RedirectToAction("Index");

        }
        public IActionResult changePhone()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult changePhone(USER obj)
        {
            int account_id = (int)HttpContext.Session.GetInt32("User_Reg_Id");
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);
            USER entity =_UserRepository.GetUserById(a.User_Id);
            entity.Id = a.User_Id;
            entity.Name = obj.Name;
            entity.Address = obj.Address;
            entity.Phone = obj.Phone;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

           _UserRepository.UpdateUser(entity);

            return RedirectToAction("Index");

        }
    }
}
