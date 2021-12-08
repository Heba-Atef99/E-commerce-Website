using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class SQLTypeRepository : ITypeRepository
    {
        private readonly APDbContext _apdb;
        private readonly SADbContext _sadb;

        public SQLTypeRepository(APDbContext db1, SADbContext db2)
        {
            _apdb = db1;
            _sadb = db2;
        }

        public int AddType(TYPE t)
        {
            List<TYPE> check1 = _apdb.TYPE.Where(i => i.Type == t.Type).ToList();
            List<TYPE> check2 = _sadb.TYPE.Where(i => i.Type == t.Type).ToList();
            if (check1.Any() == false && check2.Any() == false)
            {

                List<int> maxId;
                if (t.Type[0] <= 'M')
                {
                    maxId = _sadb.TYPE.Select(i => i.Id).ToList();
                    maxId.Sort();
                    t.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 2;

                    _sadb.TYPE.Add(t);
                    _sadb.SaveChanges();
                }
                else
                {
                    maxId = _apdb.TYPE.Select(i => i.Id).ToList();
                    maxId.Sort();
                    t.Id = (maxId.Any() == true) ? maxId.Last() + 2 : 1;

                    _apdb.TYPE.Add(t);
                    _apdb.SaveChanges();
                }

                return t.Id;
            }
            return 0;
        }

        public IEnumerable<TYPE> GetAllTypes()
        {
            List<TYPE> types1 = _apdb.TYPE.ToList();
            List<TYPE> types2 = _sadb.TYPE.ToList();

            IEnumerable<TYPE> types = types1.Concat(types2);
            return types;
        }

        public TYPE GetItemById(int typeId)
        {
            TYPE t;

            if (typeId % 2 == 0)
            {
                t = _sadb.TYPE.Where(i => i.Id == typeId).Single();
            }
            else
            {
                t = _apdb.TYPE.Where(i => i.Id == typeId).Single();
            }

            return t;
        }

    }
}
