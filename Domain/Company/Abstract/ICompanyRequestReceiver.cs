using Domain.Client;

namespace Domain.Company.Abstract;

public interface ICompanyRequestReceiver
{
    Guid ReceiveProject(ClientProject clientProject, BaseClient projectOwner);
}