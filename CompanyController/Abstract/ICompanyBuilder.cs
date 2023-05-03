using Domain.Common.Abstract;
using Domain.Company;
using Domain.Company.Abstract;

namespace CompanyController.Abstract;

public interface ICompanyBuilder
{
    void SetCompanyName(string name);
    void AddLogger(ILogger logger);
    IDepartment CreateDepartmentWithRandomCommands(string departmentName);
    Company Build();
}