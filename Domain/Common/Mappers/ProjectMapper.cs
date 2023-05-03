using Domain.Client;
using Domain.Company;

namespace Domain.Common.Mappers;

public static class ProjectMapper
{
    public static CompanyProject ToCompanyProject(this ClientProject clientProject, BaseClient projectOwner)
    {
        return new CompanyProject(clientProject.Id, clientProject.Title, projectOwner, clientProject.ExpectedPrice, 
            clientProject.Deadline);
    }
}