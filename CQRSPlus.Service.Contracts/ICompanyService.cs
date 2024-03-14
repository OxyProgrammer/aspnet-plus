using CQRSPlus.Shared.DataTransferObjects;

namespace CQRSPlus.Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    }
}
