using CompanyController.Abstract;
using CompanyController.Models;
using Domain.Client;
using Domain.Common.Abstract;
using Domain.Company;
using ItCompany.UI.Models;
using Mappers;

namespace CompanyController;

public class DomainController : IController
{
    private readonly ILogger _logger;
    private readonly List<Company> _companies;
    private DomainCounter _domainCounter;
    private const int QuantityOfDepartments = 5;
    private const int ClientMinMoney = 100000;
    private const int CLientMaxMoney = 500000;

    public DomainController(ILogger logger)
    {
        _logger = logger;
        _companies = new List<Company>();
        _domainCounter = new();
    }

    public CompanyViewModel ConfigureClients(int countOfClients, CompanyViewModel company)
    {
        Company? companyToBind = _companies.FirstOrDefault(x => x.Id == company.Id);
        ThrowExceptionIfThereIsNoSuchCompany(companyToBind);

        for (int i = 0; i < countOfClients; i++)
        {
            var client = new Client(companyToBind, Guid.NewGuid(), $"User #{_domainCounter.UsernameCount++}"
                , Random.Shared.Next(ClientMinMoney, CLientMaxMoney));
            OrderProjects(client, 2);
        }

        return companyToBind.ToModel();
    }

    public void StartProcess(CompanyViewModel company, Guid projectId)
    {
        var domain = _companies.FirstOrDefault(x => x.Id == company.Id);
        ThrowExceptionIfThereIsNoSuchCompany(domain);
        domain.StartWorkOnProject(projectId);
    }

    public CompanyViewModel ConfigureCompany()
    {
        var companyBuilder = new CompanyBuilder();
        companyBuilder.SetCompanyName($"Company: #{_domainCounter.CompanyCount++}");
        for (int i = 0; i < QuantityOfDepartments; i++)
        {
            companyBuilder.CreateDepartmentWithRandomCommands($"Department #{_domainCounter.DepartmentCount++}");
        }

        companyBuilder.AddLogger(_logger);

        var company = companyBuilder.Build();
        _companies.Add(company);
        return company.ToModel();
    }

    public CompanyViewModel? GetCurrentCompanyState(Guid id)
    {
        return _companies.FirstOrDefault(x => x.Id == id)?.ToModel();
    }

    public CompanyViewModel? CreateAndOrderProjects(Guid companyId, int projectsCount)
    {
        var company = _companies.FirstOrDefault(x => x.Id == companyId);
        if (company == null)
        {
            return null;
        }

        foreach (var item in company.GetClients())
        {
            OrderProjects(item, Random.Shared.Next(1, 3));
        }

        return company.ToModel();
    }

    private void OrderProjects(BaseClient client, int projectsCount)
    {
        for (int i = 0; i < projectsCount; i++)
        {
            client.OrderProject(new ClientProject(Guid.NewGuid(), $"Project #{_domainCounter.ProjectCount++}",
                Random.Shared.Next(1800, 4500), DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(14, 141)))));
        }
    }

    private void ThrowExceptionIfThereIsNoSuchCompany(Company? company)
    {
        if (company == null)
        {
            throw new Exception("There is no such company");
        }
    }
}
