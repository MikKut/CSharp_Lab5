using Domain.Client;
using Domain.Company;
using Domain.Company.Abstract;
using ItCompany.UI.Models;

namespace Mappers;

public static class ClientMapper
{
    public static Client ToDomain(this ClientViewModel model, ICompanyRequestReceiver receiver)
    {
        return new Client(receiver, model.Id, model.Name, model.Money);
    }
    public static ClientViewModel ToModel(this BaseClient domain)
    {
        return new ClientViewModel 
        { 
            Id = domain.Id, 
            Name = domain.Username, 
            Money = domain.Money, 
            Projects = domain.Select(x => x.ToModel()).ToList()
        };
    }
}
