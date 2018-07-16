using System.Collections.Generic;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class TransactionTypeRepository : IRepository<TransactionType>
    {
        private readonly VacationsContext _context;

        public TransactionTypeRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<TransactionType> GetAll()
        {
            return _context.TransactionTypes.ToList();
        }

        public TransactionType GetById(string id)
        {
            return _context.TransactionTypes.FirstOrDefault(x => x.TransactionTypeID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.TransactionTypes.FirstOrDefault(x => x.TransactionTypeID == id);

            if (obj != null)
            {
                _context.TransactionTypes.Remove(obj);
            }
        }

        public void Add(TransactionType TransactionType)
        {
            _context.TransactionTypes.Add(TransactionType);
        }
    }
}
