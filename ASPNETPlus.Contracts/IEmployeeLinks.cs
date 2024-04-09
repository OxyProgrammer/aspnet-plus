using ASPNETPlus.Entities.LinkModels;
using ASPNETPlus.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace ASPNETPlus.Contracts
{
    public interface IEmployeeLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto,
        string fields, Guid companyId, HttpContext httpContext);
    }

}
