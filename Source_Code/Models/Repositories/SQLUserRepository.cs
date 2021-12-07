using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLUserRepository : IUserRepositorycs
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLUserRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        //Creat
        public void AddUser(USER u)
        {
            int maxId = 0;
            if(u.Name[0] <= 'M')
            {
                maxId = _sadb.USER.Select(u => u.Id).DefaultIfEmpty().Max();
                u.Id = (maxId > 0) ? maxId + 2 : 2;
                _sadb.USER.Add(u);
                _sadb.SaveChanges();
            }
            else
            {
                maxId = _apdb.USER.Select(u => u.Id).DefaultIfEmpty().Max();
                u.Id = (maxId > 0) ? maxId + 2 : 1;

                _apdb.USER.Add(u);
                _apdb.SaveChanges();
            }
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
            Boolean check = DeleteUser(oldId);
            if (check)
            {
                AddUser(newUser);
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
