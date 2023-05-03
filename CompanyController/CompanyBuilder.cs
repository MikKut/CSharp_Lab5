using CompanyController.Abstract;
using Domain.Common.Abstract;
using Domain.Company;
using Domain.Company.Abstract;

namespace CompanyController;

internal class CompanyBuilder : ICompanyBuilder
{
    private Company _company;

    public CompanyBuilder()
    {
        _company = new Company(Guid.NewGuid(), "The Company");
    }

    public void AddLogger(ILogger logger)
    {
        _company.SetLogger(logger);
    }

    public Company Build()
    {
        return _company;
    }

    public IDepartment CreateDepartmentWithRandomCommands(string departmentName)
    {
        var department = new Department(Guid.NewGuid(), departmentName, _company);
        for (int i = 0; i < Random.Shared.Next(1, 4); i++)
        {
            department.AddCommand(new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7)));
        }

        _company.AddDepartment(department);
        return department;
    }

    public void SetCompanyName(string name)
    {
        _company.ChangeCompanyName(name);
    }
}
