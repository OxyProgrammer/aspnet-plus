using CQRSPlus.Contracts;
using CQRSPlus.LoggerService;
using CQRSPlus.Service.Contracts;

namespace CQRSPlus.Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CompanyService(IRepositoryManager repository, ILoggerManager  logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }

}
