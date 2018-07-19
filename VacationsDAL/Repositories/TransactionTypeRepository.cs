﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
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

        public void Save()
        {
            if (_context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                                || e.State == EntityState.Modified
                                                || e.State == EntityState.Deleted))
            {
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
