using ASPNETPlus.Entities.LinkModels;
using ASPNETPlus.Entities.Models;
using ASPNETPlus.Shared.DataTransferObjects;
using ASPNETPlus.Shared.RequestFeatures;
using System.Dynamic;

namespace ASPNETPlus.Service.Contracts
{
    public interface IEmployeeService
    {
        //Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(Guid companyId, LinkParameters linkParameters, bool trackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByIdsAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<EmployeeDto> employees, string ids)> CreateEmployeesForCompanyCollectionAsync(Guid companyId, IEnumerable<EmployeeForCreationDto> companyCollection);
        Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
    }
}
