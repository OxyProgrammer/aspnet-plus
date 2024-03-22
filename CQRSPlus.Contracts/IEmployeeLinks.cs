using CQRSPlus.Entities.LinkModels;
using CQRSPlus.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace CQRSPlus.Contracts
{
    public interface IEmployeeLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto,
        string fields, Guid companyId, HttpContext httpContext);
    }

}
