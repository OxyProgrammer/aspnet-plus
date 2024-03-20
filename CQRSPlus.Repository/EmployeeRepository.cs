﻿using CQRSPlus.Contracts;
using CQRSPlus.Entities.Models;
using CQRSPlus.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CQRSPlus.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId) && (e.Age>= employeeParameters.MinAge && e.Age <= employeeParameters.MaxAge), trackChanges)
                .OrderBy(e => e.Name)
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync();
            var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).CountAsync();
            return new PagedList<Employee>(employees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
             await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

        public async Task<IEnumerable<Employee>> GetEmployeesByIdAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges) =>
          await FindByCondition(e => ids.Contains(e.Id) && e.CompanyId.Equals(companyId), trackChanges)
         .ToListAsync();
    }

}
