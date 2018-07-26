using System;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly VacationsContext _context;

        public JobTitleRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public JobTitle[] Get(Func<JobTitle, bool> whereCondition = null)
        {
            IQueryable<JobTitle> baseCondition = _context.JobTitles;
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
        }

        public JobTitle GetById(string id)
        {
            return _context.JobTitles.FirstOrDefault(x => x.JobTitleID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.JobTitles.FirstOrDefault(x => x.JobTitleID == id);

            if (obj != null)
            {
                _context.JobTitles.Remove(obj);
                _context.SaveChanges();
            }
        }

        public void Add(JobTitle jobTitle)
        {
            _context.JobTitles.Add(jobTitle);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
