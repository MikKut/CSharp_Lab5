using Models.Interfaces;

namespace ItCompany.UI.Models;

public class ClientViewModel : ICompanyViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Money { get; set; }
    public List<ProjectViewModel>? Projects { get; set; }
    public override string ToString()
    {
        return Name;
    }

    public string GetAllDataInStringFormat()
    {
        return $"Id: {Id}\nName: {Name}\nMoney: {Money}\nProject count: {Projects?.Count ?? 0}";
    }
}