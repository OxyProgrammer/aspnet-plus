using CQRSPlus.Entities.Models;

namespace CQRSPlus.Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }
}
