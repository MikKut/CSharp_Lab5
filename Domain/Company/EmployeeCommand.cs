using Domain.Company.Abstract;

namespace Domain.Company;

public class EmployeeCommand : BaseEmployeeCommand
{
    public EmployeeCommand(Guid id, int employeeCount) : base(id, employeeCount)
    {
    }
}