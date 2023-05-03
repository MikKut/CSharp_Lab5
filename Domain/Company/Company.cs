using Domain.Client;
using Domain.Common.Abstract;
using Domain.Common.Mappers;
using Domain.Company.Abstract;
using System.Collections.ObjectModel;

namespace Domain.Company;

public class Company : BaseCompany, IMoneyWithdrawer
{
    private readonly object balanceLock = new object();
    private List<BaseClient> _clients;
    private List<CompanyProject> _projects;
    protected List<IDepartment> _departments;
    private ILogger? _logger;

    private readonly int _projectLimit;
    public Company(Guid id, string name, int projectLimit = 100) : base(id, name)
    {
        _clients = new List<BaseClient>();
        _projects = new List<CompanyProject>();
        _departments = new List<IDepartment>();
        _projectLimit = projectLimit;
    }
    
    public void SetLogger(ILogger logger)
    {
        _logger = logger;

        foreach (var item in _departments)
        {
            item.SetLogger(logger);
        }
    }

    public override Guid ReceiveProject(ClientProject clientProject, BaseClient projectOwner)
    {
        lock (balanceLock)
        {
            if (CanAcceptProject() == false)
            {
                return Guid.Empty;
            }

            AddClient(projectOwner);

            var departmentToReceive = GetAvailableDepartment();
            if (departmentToReceive == null)
            {
                throw new InvalidOperationException("There is no available departments");
            }

            var project = clientProject.ToCompanyProject(projectOwner);
            _logger?.LogInformation($"{project.Title}: Total price for project: {project.TotalPrice}");

            if (projectOwner.Money < project.TotalPrice)
            {
                _logger?.LogError($"{project.Title}: Can not start working on project. " +
                    $"Client does not have enough money");
                return Guid.Empty;
            }

            departmentToReceive.ReceiveProject(project);
            _projects.Add(project);

            return project.Id;
        }
    }
    public override void AddDepartment(IDepartment department)
    {
        _departments.Add(department);
    }

    private void AddClient(BaseClient client)
    {
        var exists = _clients.FirstOrDefault(x => x.Id == client.Id);

        if (exists != null)
        {
            return;
        }

        lock(client)
        {
            _clients.Add(client);
        }
    } 

    protected virtual bool CanAcceptProject()
    {
        var exist = GetAvailableDepartment();

        if (exist == null)
        {
            return false;
        }

        return (_projects.Count + 1) < _projectLimit;
    }
    protected virtual IDepartment? GetAvailableDepartment()
    {
        return _departments.FirstOrDefault(x => x.CanReceiveProject() == true);
    }

    public override IEnumerable<CompanyProject> GetAllProjects()
    {
        return new ReadOnlyCollection<CompanyProject>(_projects);
    }

    public bool WithdrawMoney(CompanyProject project, double money)
    {
        var client = project.ProjectOwner;
        return client.WithdrawMoney(money);
    }

    public override void StartWorkOnProject(Guid projectId)
    {
        var departmentWithProject = _departments.FirstOrDefault(x => x.HasProject(projectId));

        if (departmentWithProject == null)
        {
            throw new InvalidOperationException();
        }

        departmentWithProject.StartWorkOnProject();
    }

    public override IEnumerable<BaseClient> GetClients()
    {
        return _clients;
    }

    public override IEnumerable<IDepartment> GetDepartments()
    {
        return _departments;
    }
}