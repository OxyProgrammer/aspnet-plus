using CQRSPlus.Entities.Models;

namespace CQRSPlus.Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);

    }
}
