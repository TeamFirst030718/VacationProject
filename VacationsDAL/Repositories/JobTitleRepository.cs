using System.Collections.Generic;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class JobTitleRepository : IRepository<JobTitle>
    {
        private readonly VacationsContext _context;

        public JobTitleRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<JobTitle> GetAll()
        {
            return _context.JobTitles.ToList();
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
            }
        }

        public void Add(JobTitle jobTitle)
        {
            _context.JobTitles.Add(jobTitle);
        }
    }
}
