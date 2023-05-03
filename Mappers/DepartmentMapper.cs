using Domain.Company;
using Domain.Company.Abstract;
using ItCompany.UI.Models;

namespace Mappers;

public static class DepartmentMapper
{
    public static Department ToDomain(this DepartmentViewModel model, IMoneyWithdrawer moneyWithdraw, ICompanyRequestReceiver receiver)
    {
        return new Department(model.Id, model.Name, moneyWithdraw, model.Project.ToDomain(receiver));
    }
    public static DepartmentViewModel ToModel(this Department domain)
    {
        return new DepartmentViewModel
        {
            Id = domain.Id,
            Name = domain.Name,
            CountOfCommands = domain.Count(),
            Project = domain.Project?.ToModel()
        };
    }
}
