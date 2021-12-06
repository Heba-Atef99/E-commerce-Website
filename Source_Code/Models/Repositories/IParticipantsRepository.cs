using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IParticipantsRepository
    {
        void Add(Participants p);
        IEnumerable<Participants> GetParticipants();
    }
}
