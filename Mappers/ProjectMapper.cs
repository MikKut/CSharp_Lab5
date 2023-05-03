using Domain.Client;
using Domain.Company;
using Domain.Company.Abstract;
using ItCompany.UI.Models;

namespace Mappers;

public static class ProjectMapper
{
    public static CompanyProject ToDomain(this ProjectViewModel model, ICompanyRequestReceiver receiver)
    {
        return new CompanyProject(model.Id, model.Name, model.ProjectOwner.ToDomain(receiver),
            model.TotalPrice, model.Deadline);
    }
    public static ProjectViewModel ToModel(this CompanyProject domain)
    {
        return new ProjectViewModel
        {
            Id = domain.Id,
            Name = domain.Title,
            Deadline = domain.Deadline,
            ProjectOwner = domain.ProjectOwner.ToModel(),
            Status = domain.Status.ToString(),
            TotalPrice = domain.TotalPrice,
            CountOfIteration = CompanyProject.CalculateIterations(domain.Deadline)
        };
    }
    public static ProjectViewModel ToModel(this ClientProject domain)
    {
        return new ProjectViewModel
        {
            Id = domain.Id,
            Name = domain.Title,
            Deadline = domain.Deadline,
            ProjectOwner = null,
            Status = "To do",
            TotalPrice = CompanyProject.CalculateTotalPrice(domain.ExpectedPrice, 
                CompanyProject.EvaluateComplexity(domain.Deadline), domain.Deadline),
            CountOfIteration = CompanyProject.CalculateIterations(domain.Deadline)
        };
    }
}