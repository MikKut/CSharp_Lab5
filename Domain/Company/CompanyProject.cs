using Domain.Client;
using Domain.Common;
using Domain.Common.Abstract;

namespace Domain.Company;

public class CompanyProject : BaseDomainObject
{
    /// <summary>
    /// One iteration per week
    /// </summary>
    private const int AcceptableCountOfIterations = 6;
    private const double ProgressIsDone = 100;
    private const int DateForOneIteration = 7;
    public string Title { get; private set; }
    public DateOnly Deadline { get; private set; }
    public int IterationCount { get; }
    public int TotalPrice { get; }
    public Complexity Complexity { get; }
    private double _progress;
    public ProjectStatus Status { get; private set; }
    public BaseClient ProjectOwner { get; }
    public CompanyProject(Guid id, string title, BaseClient owner, int expectedPrice, DateOnly deadline) : base(id)
    {
        Title = title;
        ProjectOwner = owner;
        Complexity = EvaluateComplexity(deadline);
        TotalPrice = CalculateTotalPrice(expectedPrice, Complexity, deadline);
        IterationCount = CalculateIterations(deadline);
        Deadline = deadline;

        Status = ProjectStatus.Todo;
        _progress = 0;
    }

    public double UpdateProgress()
    {
        Status = ProjectStatus.InProcess;
        var _progressForTheStep = CalculateProgressForStep(IterationCount);
        if (_progress + _progressForTheStep >= ProgressIsDone)
        {
            _progress = ProgressIsDone;
            Status = ProjectStatus.Done;
            return _progress;
        }
        
        _progress += _progressForTheStep;
        return _progress;
    }

    private double CalculateProgressForStep(int countOfIteration)
    {
        return ProgressIsDone / countOfIteration;
    }

    public double CalculatePricePerIteration()
    {
        return TotalPrice / IterationCount;
    }

    public static int CalculateIterations(DateOnly deadline)
    {
        var currentDateTime = DateTime.Now;
        int counterOfIterations = 0;
        while (DateOnly.FromDateTime(currentDateTime) < deadline)
        {
            currentDateTime = currentDateTime.AddDays(DateForOneIteration);
            counterOfIterations += 1;
        }

        return counterOfIterations;
    }
    public static int CalculateTotalPrice(int expectedPrice, Complexity complexity, DateOnly deadline)
    {
        double coefficient = CalculateCoefficient(deadline);

        int lowerPrice = (int)(expectedPrice - (expectedPrice * coefficient));
        int upperPrice = (int)(expectedPrice + (expectedPrice * coefficient));

        if (complexity == Complexity.Easy)
        {
            return Random.Shared.Next(lowerPrice, expectedPrice);
        }
        
        if (complexity == Complexity.Medium)
        {
            return Random.Shared.Next(lowerPrice, expectedPrice + 20);
        }

        return Random.Shared.Next(expectedPrice, upperPrice);
    }
    public static double CalculateCoefficient(DateOnly deadline)
    {
        if (CalculateIterations(deadline) < AcceptableCountOfIterations)
        {
            return 0.6;
        }
        else
        {
            return 0.4;
        }
    }
    public static Complexity EvaluateComplexity(DateOnly deadline)
    {
        int daysLeft = deadline.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;
        if (daysLeft > 90)
        {
            return Complexity.Easy;
        }

        if (daysLeft > 45)
        {
            return Complexity.Medium;
        }

        return Complexity.Hard;
    }
}