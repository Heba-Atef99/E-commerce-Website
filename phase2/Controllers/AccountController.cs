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
        //private readonly IAccountRepository _logger;
        //private readonly IUserRepository _user;
        //private readonly IItemRepository _item;
        //public AccountController(IAccountRepository logger, IUserRepository user, IItemRepository item)
        //{
        //    _logger = logger;
        //    _user = user;
        //    _item = item;
        //}
        private readonly IItemRepository _itemRepository;
        private readonly IPromotedItemRepository _PromotedItemRepository;
        private readonly ICartRepository _CartRepository;
        private readonly IAccountRepository _AccountRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ITypeRepository _TypeRepository;
        public AccountController(IItemRepository itemRepository,
            IPromotedItemRepository PromotedItemRepoRepository, ICartRepository CartRepository,
            IAccountRepository AccountRepository, IUserRepository UserRepository,
            ITypeRepository TypeRepository)
        {
            _itemRepository = itemRepository;
            _PromotedItemRepository = PromotedItemRepoRepository;
            _CartRepository = CartRepository;
            _AccountRepository = AccountRepository;
            _UserRepository = UserRepository;
            _TypeRepository = TypeRepository;
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ITEM>> Owner(int id)
        {
            return _itemRepository.GetItemsByAccId(id).ToArray();

        }
        [HttpGet("{id}/{item_id}")]
        public ActionResult<IEnumerable<PROMOTED_ITEM>> Share_Item(int id,int item_id)
        {
            PROMOTED_ITEM promoted = new PROMOTED_ITEM();
            promoted.Item_Id = item_id;
            promoted.Promoted_Account_Id = id;
            _PromotedItemRepository.AddPromotedItem(promoted);
            return _PromotedItemRepository.GetPromotedItemsByAccId(id).ToArray();

        }
        [HttpGet("{id}/{item_id}")]
        public ActionResult<IEnumerable<CART>> Add_To_Cart(int id, int item_id)
        {
            CART cart = new CART();
            cart.Item_Id = item_id;
            cart.Account_Id = id;
            cart.Item_count = 1;
            _CartRepository.AddToCart(cart);
            return _CartRepository.GetCartByAccId(id).ToArray();

        }
        [HttpGet("{id}")]
        public ActionResult<int> Deposit(int id)
        {
            return _AccountRepository.GetAccountByAccId(id).Balance;

        }
        [HttpPost]
        public void Deposit(ACCOUNT ob)
        {
            int id = ob.Id;
            //addbalance = ob;
            ACCOUNT account = _AccountRepository.GetAccountByAccId(id);
            //account.Balance = account.Balance + ob.Balance;
            account.Balance = account.Balance + ob.Balance;
            bool k = _AccountRepository.UpdateAccount(account, 0);
            //return CreatedAtAction("Deposit", new ACCOUNT { Id = ob.Id }, ob);
            //return RedirectToAction("Deposit");
            //return RedirectToActionResult("Deposit",id);
        }

    }
}
