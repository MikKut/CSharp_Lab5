using Domain.Client;
using Domain.Company.Abstract;
using ItCompany.UI.Models;

namespace CompanyController.Abstract;

public interface IController
{
    CompanyViewModel ConfigureCompany();
    CompanyViewModel? GetCurrentCompanyState(Guid id);
    CompanyViewModel ConfigureClients(int countOfClients, CompanyViewModel companyToBind);
    void StartProcess(CompanyViewModel company, Guid projectId);

    CompanyViewModel? CreateAndOrderProjects(Guid companyId, int projectsCount);
}