using System.Collections;
using Domain.Common;
using Domain.Common.Abstract;

namespace Domain.Client;

public class ClientProject : BaseDomainObject
{
    public string Title { get; private set; }
    public int ExpectedPrice { get; private set; }
    public DateOnly Deadline { get; private set; }
    
    public ClientProject(Guid id, string title, int expectedPrice, DateOnly deadline) : base(id)
    {
        Title = title;
        ExpectedPrice = expectedPrice;
        Deadline = deadline;
    }
}