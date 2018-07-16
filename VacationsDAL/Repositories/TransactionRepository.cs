using System.Collections.Generic;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly VacationsContext _context;

        public TransactionRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public Transaction GetById(string id)
        {
            return _context.Transactions.FirstOrDefault(x => x.TransactionID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.Transactions.FirstOrDefault(x => x.TransactionID == id);

            if (obj != null)
            {
                _context.Transactions.Remove(obj);
            }
        }

        public void Add(Transaction Transaction)
        {
            _context.Transactions.Add(Transaction);
        }
    }
}
