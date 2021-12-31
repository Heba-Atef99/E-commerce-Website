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
        [HttpGet("{account_id}")]
        public ActionResult<displayed_profile> Profile(int account_id)
        {

            var a = _AccountRepository.GetAccountByAccId(account_id);
            USER b = _UserRepository.GetUserById(a.User_Id);
            displayed_profile result = new displayed_profile();
            result.Address = b.Address;
            result.Name = b.Name;
            result.Phone = b.Phone;
            result.Balance = a.Balance;
            result.Email = a.Email;
            result.Acc_Id = a.Id;
            return result;
        }

        [HttpPost]
        public ActionResult<int> ChangeName(displayed_changename s)
        {

            ACCOUNT a = _AccountRepository.GetAccountByAccId(s.account_id);
            USER entity = _UserRepository.GetUserById(a.User_Id);



            entity.Name = s.name;



            _UserRepository.UpdateUser(entity);

            //_UserRepository.UpdateUserName(61,entity);




            return 0;
        }

        //POST
        [HttpPost]

        public ActionResult<int> ChangePassword(displayed_changepassword s)
        {

            ACCOUNT a = _AccountRepository.GetAccountByAccId(s.account_id);

            if (BCrypt.Net.BCrypt.Verify(s.currentpassword, a.Pass))
            {
                if (s.newpassword == s.reenternewpassword)
                {
                    a.Pass = BCrypt.Net.BCrypt.HashPassword(s.newpassword);


                    _AccountRepository.UpdateAccount(a, 1);


                }
                else
                {

                    return NotFound();
                }
            }
            else
            {

                return NotFound();
            }

            return 5;

        }

        [HttpPost]
        public ActionResult<int> ChangeEmail(displayed_changeemail s)
        {


            ACCOUNT entity = _AccountRepository.GetAccountByAccId(s.account_id);

            entity.Email = s.email;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);
            _AccountRepository.UpdateAccount(entity, 1);


            return 1;

        }



        [HttpPost]

        public ActionResult<int> ChangeAddress(displayed_changeaddress s)
        {

            ACCOUNT a = _AccountRepository.GetAccountByAccId(s.account_id);
            USER entity = _UserRepository.GetUserById(a.User_Id);

            entity.Address = s.address;

            //entity.USER =_UserRepository.GetUserById(entity.User_Id);

            _UserRepository.UpdateUser(entity);

            return 2;

        }


        [HttpPost]

        public ActionResult<int> ChangePhone(displayed_changephone s)
        {

            ACCOUNT a = _AccountRepository.GetAccountByAccId(s.account_id);
            USER entity = _UserRepository.GetUserById(a.User_Id);


            entity.Phone = s.phone;



            _UserRepository.UpdateUser(entity);

            return 3;

        }

        [HttpGet("{account_id}")]
        public ActionResult<displayed_cart>/*<Tuple<List<displayed_item_cart>,int,int>>*/ Cart(int account_id)
        {


            displayed_cart displayed_Cart = new displayed_cart();
            List<displayed_item_cart> list_items = new List<displayed_item_cart>();

            //List<ITEM> list_items = new List<ITEM>();

            int total_price = 0;
            ACCOUNT a = _AccountRepository.GetAccountByAccId(account_id);


            IEnumerable<CART> b = _CartRepository.GetCartByAccId(account_id);



            List<int> array = new List<int>();
            bool flag = false;
            foreach (CART cart in b)
            {
                ITEM item = _itemRepository.GetItemById(cart.Item_Id);
                flag = false;
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i] == cart.Item_Id)
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    array.Add(cart.Item_Id);
                    displayed_item_cart item_Cart = new displayed_item_cart();
                    item_Cart.description = item.Description;
                    item_Cart.name = item.Name;
                    item_Cart.price = item.Price;
                    item_Cart.image = item.Image;
                    item_Cart.item_count = cart.Item_count;
                    list_items.Add(item_Cart);
                    //list_items.Add(item);
                    total_price += (item.Price * item_Cart.item_count);
                }
            }




            //var result = Tuple.Create(list_items, a.Balance, total_price);
            displayed_Cart.balance = a.Balance;
            displayed_Cart.total_price = total_price;
            displayed_Cart.items = list_items;
            return displayed_Cart;
        }



        [HttpPost]

        public ActionResult<int> RemoveCartItem(displayed_removeitem s)
        {

            _CartRepository.DeleteItemFromCart(s.account_id, s.item_id);

            return 4;
        }

    }
}
