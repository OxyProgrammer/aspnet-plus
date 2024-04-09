using ASPNETPlus.Entities.Models;
using ASPNETPlus.Shared.RequestFeatures;

namespace ASPNETPlus.Contracts
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<IEnumerable<Employee>> GetEmployeesByIdAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
