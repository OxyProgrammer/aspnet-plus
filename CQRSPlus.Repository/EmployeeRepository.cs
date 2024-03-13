using CQRSPlus.Contracts;
using CQRSPlus.Entities.Models;

namespace CQRSPlus.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }

}
