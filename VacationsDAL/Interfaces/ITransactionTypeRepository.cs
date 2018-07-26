using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface ITransactionTypeRepository : IRepository<TransactionType>
    {
        TransactionType GetByType(string type);
    }
}
