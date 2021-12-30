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
        [HttpPost]
        public void Share_Item(PROMOTED_ITEM ob)
        {
            _PromotedItemRepository.AddPromotedItem(ob);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PROMOTED_ITEM>> Share_Item(int id)
        {
            return _PromotedItemRepository.GetPromotedItemsByAccId(id).ToArray();

        }
        [HttpPost]
        public void Add_To_Cart(CART ob)
        {
            _CartRepository.AddToCart(ob);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CART>> Add_To_Cart(int id)
        {
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
            ACCOUNT account = _AccountRepository.GetAccountByAccId(id);
            account.Balance = account.Balance + ob.Balance;
            bool k = _AccountRepository.UpdateAccount(account, 0);
           
        }

    }
}
