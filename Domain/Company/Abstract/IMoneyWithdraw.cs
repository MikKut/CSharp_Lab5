namespace Domain.Company.Abstract;

public interface IMoneyWithdrawer
{
    bool WithdrawMoney(CompanyProject project, double money);
}
