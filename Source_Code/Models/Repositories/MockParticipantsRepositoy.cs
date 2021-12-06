using E_commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace E_commerce.Models.Repositories
{
    public class MockParticipantsRepositoy : IParticipantsRepository
    {
        private readonly APDbContext _apdb;

        public MockParticipantsRepositoy(APDbContext db1)
        {
            _apdb = db1;
        }

        public void Add(Participants p)
        {
            _apdb.Participants.Add(p);
            _apdb.SaveChanges();

            //throw new NotImplementedException();
        }

        public IEnumerable<Participants> GetParticipants()
        {
            return _apdb.Participants;
            throw new NotImplementedException();
        }
    }
}
