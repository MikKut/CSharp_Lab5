using Domain.Company;
using Domain.Company.Abstract;
using ItCompany.UI.Models;

namespace Mappers;

public static class CompanyMapper
{
    public static Company ToDomain(this CompanyViewModel viewModel)
    {
        return new Company(viewModel.Id, viewModel.Name);
    }
    public static CompanyViewModel ToModel(this Company domain)
    {
        return new CompanyViewModel
        {
            Id = domain.Id,
            Name = domain.Name,
            Projects = domain.GetAllProjects().Select(x => x.ToModel()).ToList(),
            Departments = domain.GetDepartments().Select(x => ((Department)x).ToModel()).ToList(),
            Clients = domain.GetClients().Select(x => x.ToModel()).ToList()
        };
    }
}
