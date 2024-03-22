using CQRSPlus.Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace CQRSPlus.Entities.LinkModels
{
    public record LinkParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
}
