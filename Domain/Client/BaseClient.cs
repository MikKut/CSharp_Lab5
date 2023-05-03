using Domain.Common.Abstract;
using System.Collections;

namespace Domain.Client;

public abstract class BaseClient : BaseDomainObject, IEnumerable<ClientProject>
{
    private readonly object balanceLock = new object();

    public string Username { get; protected set; }
    public double Money { get; private set; }
    protected BaseClient(Guid id, string username, double money) : base(id)
    {
        Username = username;
        Money = money;
    }

    public virtual void ChangeUsername(string newUsername)
    {
        Username = newUsername;
    }
    public abstract Guid OrderProject(ClientProject project);
    public virtual void AddMoney(double money)
    {
        if (money < 0)
            throw new InvalidOperationException();

        lock (balanceLock)
        {
            Money += money;
        }
    }
    public virtual bool WithdrawMoney(double money)
    {
        if (Money - money <= 0)
        {
            return false;
        }

        lock (balanceLock)
        {
            Money -= money;
        }

        return true;
    }

    public abstract IEnumerator<ClientProject> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}