using Microsoft.Extensions.Logging;
using ILogger = Domain.Common.Abstract.ILogger;

namespace DomainConsoleTest.Logger;

public class CompanyLogger : ILogger
{
    private readonly ILogger<CompanyLogger> _logger;
    public CompanyLogger(ILogger<CompanyLogger> logger)
    {
        _logger = logger;
    }
    
    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogError(string message)
    {
        _logger.LogError(message);
    }
}