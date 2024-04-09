using ASPNETPlus.Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace ASPNETPlus.Entities.LinkModels
{
    public record LinkParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
}
