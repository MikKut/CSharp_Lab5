using Domain.Client;
using Domain.Common.Abstract;

namespace Domain.Company.Abstract;

public abstract class BaseCompany : BaseDomainObject, ICompanyRequestReceiver
{
    public string Name { get; protected set; }
    public BaseCompany(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public virtual void ChangeCompanyName(string newTitle)
    {
        Name = newTitle;
    }
    public abstract Guid ReceiveProject(ClientProject clientProject, BaseClient projectOwner);
    public abstract void StartWorkOnProject(Guid projectId);
    public abstract void AddDepartment(IDepartment department);
    public abstract IEnumerable<CompanyProject> GetAllProjects();
    public abstract IEnumerable<BaseClient> GetClients();
    public abstract IEnumerable<IDepartment> GetDepartments();
}