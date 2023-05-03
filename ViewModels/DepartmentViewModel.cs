using Models.Interfaces;

namespace ItCompany.UI.Models;

public class DepartmentViewModel : ICompanyViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CountOfCommands { get; set; }
    public ProjectViewModel? Project { get; set; }
    public override string ToString()
    {
        return Name;
    }

    public string GetAllDataInStringFormat()
    {
        return $"Id: {Id}\nDepartment name: {Name}\nCount of commands: {CountOfCommands}\n";
    }
}
