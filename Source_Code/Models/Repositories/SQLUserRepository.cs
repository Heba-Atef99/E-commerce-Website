using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLUserRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //Creat
        public int AddUser(USER u)
        {
            List<int> maxId;
            if(u.Name[0] <= 'M')
            {
                maxId = _sadb.USER.Select(i => i.Id).ToList();
                maxId.Sort();
                u.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 2;

                _sadb.USER.Add(u);
                _sadb.SaveChanges();
            }
            else
            {
                maxId = _apdb.USER.Select(i => i.Id).ToList();
                maxId.Sort();
                u.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 1;

                _apdb.USER.Add(u);
                _apdb.SaveChanges();
            }

            return u.Id;
        }

        //Read
        public IEnumerable<USER> GetAllUsers()
        {
            List<USER> db1Users = _apdb.USER.ToList();
            List<USER> db2Users = _sadb.USER.ToList();

            IEnumerable<USER> users = db1Users.Concat(db2Users);
            return users;
        }

        public USER GetUserById(int userId)
        {
            USER u;

            if (userId % 2 == 0)
            {
                u = _sadb.USER.Where(i => i.Id == userId).Single();
            }
            else
            {
                u = _apdb.USER.Where(i => i.Id == userId).Single();
            }

            return u;
        }

        public USER GetUserByPhone(string name, int phone)
        {
            USER u;
            if (name[0] <= 'M')
            {

                u = _sadb.USER.Where(i => i.Phone == phone).Single();
            }
            else
            {
                u = _apdb.USER.Where(i => i.Phone == phone).Single();
            }

            return u;
        }
        //Update
        public Boolean UpdateUser(USER updatedUser)
        {
            USER exist;
            if (updatedUser.Id % 2 == 0)
            {
                exist = _sadb.USER.Where(i => i.Id == updatedUser.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _sadb.USER.Attach(updatedUser);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                exist = _apdb.USER.Where(i => i.Id == updatedUser.Id).FirstOrDefault();
                if (exist != null)
                {
                    var modified = _apdb.USER.Attach(updatedUser);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        //in case there are other modifications than the name only to the user 
        public Boolean UpdateUserName(int oldId, USER newUser)
        {
            if (newUser.Name[0] <= 'M')
            {
                if(oldId % 2 == 0)
                {
                    //then the user stayes in same db
                    var modified = _sadb.USER.Attach(newUser);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();

                    return true;
                }
            }
            else if (newUser.Name[0] > 'M')
            {
                if (oldId % 2 != 0)
                {
                    //then the user stayes in same db
                    var modified = _apdb.USER.Attach(newUser);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _sadb.SaveChanges();

                    return true;
                }
            }

            Boolean check = DeleteUser(oldId);
            if (check)
            {
                int newId = AddUser(newUser);
                ACCOUNT2 acc = _apdb.ACCOUNT.Where(i => i.User_Id == oldId).Single();
                if(acc != null)
                {
                    acc.User_Id = newId;
                    var modified = _apdb.ACCOUNT.Attach(acc);
                    modified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;                    
                    _apdb.SaveChanges();
                }
                return true;
            }
            return false;
        }

        //Delete
        public Boolean DeleteUser(int userId)
        {
            if (userId % 2 == 0)
            {
                USER u = _sadb.USER.Where(i => i.Id == userId).FirstOrDefault();
                if (u != null)
                {
                    _sadb.USER.Remove(u);
                    _sadb.SaveChanges();
                    return true;
                }
            }
            else
            {
                USER u = _apdb.USER.Where(i => i.Id == userId).FirstOrDefault();
                if (u != null)
                {
                    _apdb.USER.Remove(u);
                    _apdb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

    }
}
