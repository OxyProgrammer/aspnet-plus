using AutoMapper;
using ASPNETPlus.Contracts;
using ASPNETPlus.Entities.Exceptions;
using ASPNETPlus.Entities.LinkModels;
using ASPNETPlus.Entities.Models;
using ASPNETPlus.LoggerService;
using ASPNETPlus.Service.Contracts;
using ASPNETPlus.Shared.DataTransferObjects;
using ASPNETPlus.Shared.RequestFeatures;

namespace ASPNETPlus.Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeLinks _employeeLinks;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IEmployeeLinks employeeLinks)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _employeeLinks = employeeLinks;
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(Guid companyId, LinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.EmployeeParameters.ValidAgeRange)
            {
                throw new MaxAgeRangeBadRequestException();
            }
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(companyId, linkParameters.EmployeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            var links = _employeeLinks.TryGenerateLinks(employeesDto, linkParameters.EmployeeParameters.Fields, companyId, linkParameters.Context);
            return (linkResponse: links, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);
            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return employee;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);
            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByIdsAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
            {
                throw new IdParametersBadRequestException();
            }
            var employeeEntities = await _repository.Employee.GetEmployeesByIdAsync(companyId, ids, trackChanges);
            if (ids.Count() != employeeEntities.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }
            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
            return employeesToReturn;
        }

        public async Task<(IEnumerable<EmployeeDto> employees, string ids)> CreateEmployeesForCompanyCollectionAsync(Guid companyId, IEnumerable<EmployeeForCreationDto> employeeCollection)
        {
            if (employeeCollection is null)
            {
                throw new EmployeeCollectionForCompanyBadRequest();
            }
            var employeeEntities = _mapper.Map<IEnumerable<Employee>>(employeeCollection);
            foreach (var employee in employeeEntities)
            {
                _repository.Employee.CreateEmployeeForCompany(companyId, employee);
            }
            await _repository.SaveAsync();
            var employeeCollectionToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
            var ids = string.Join(",", employeeCollectionToReturn.Select(c => c.Id));
            return (employees: employeeCollectionToReturn, ids: ids);
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
                bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChanges);
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);
            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChanges);
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges); new EmployeeNotFoundException(companyId);
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

        private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
        }
        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges)
        {
            var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }
            return employeeDb;
        }
    }

}
