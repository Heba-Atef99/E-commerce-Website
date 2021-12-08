using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface ITypeRepository
    {
        public int AddType(TYPE t);
        public IEnumerable<TYPE> GetAllTypes();
        public TYPE GetItemById(int typeId);
    }
}
