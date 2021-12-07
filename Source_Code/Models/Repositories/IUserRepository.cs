using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IUserRepository
    {
        public int AddUser(USER u);
        public IEnumerable<USER> GetAllUsers();
        public USER GetUserById(int userId);
        public Boolean UpdateUser(USER updatedUser);
        public Boolean UpdateUserName(int oldId, USER newUser);
        public Boolean DeleteUser(int userId);
    }
}
