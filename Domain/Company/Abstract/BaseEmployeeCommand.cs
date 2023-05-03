using Domain.Common.Abstract;

namespace Domain.Company.Abstract;

public abstract class BaseEmployeeCommand : BaseDomainObject
{
    protected int _employeeCount;
    public int EmployeeCount
    {
        get => _employeeCount;
    }
    
    public BaseEmployeeCommand(Guid id, int employeeCount) : base(id)
    {
        _employeeCount = employeeCount;
    }

    public virtual void HireEmployee()
    {
        _employeeCount += 1;
    }

    public virtual void FireEmployee()
    {
        if (_employeeCount == 0)
            throw new InvalidOperationException("Can not fire anyone, because command already have not employees");

        _employeeCount -= 1;
    }
}