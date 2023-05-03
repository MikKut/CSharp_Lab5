using Models.Interfaces;

namespace ItCompany.UI.Models;

public class ProjectViewModel : ICompanyViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly Deadline { get; set; }
    public int CountOfIteration { get; set;  }
    public int TotalPrice { get; set; }
    public string Status { get; set; }
    public ClientViewModel? ProjectOwner { get; set; }
    public override string ToString()
    {
        return Name;
    }

    public string GetAllDataInStringFormat()
    {
        return $"Id: {Id}\nName: {Name}\nDeadline: {Deadline}\nCount of iterations: {CountOfIteration}\n" +
            $"Total price: {TotalPrice}\nStatus: {Status}\nProject owner name: {ProjectOwner?.Name}";
    }
}
