using Asp.Versioning;
using CQRSPlus.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CQRSPlus.Presentation.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/companies")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesV2Controller(IServiceManager service) => _service = service;
        
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);
            var companiesV2 = companies.Select(x => $"{x.Name} V2");
            return Ok(companiesV2);
        }
    }
}
