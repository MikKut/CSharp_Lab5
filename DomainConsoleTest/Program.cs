// See https://aka.ms/new-console-template for more information

using Domain.Client;
using Domain.Company;
using Domain.Company.Abstract;
using DomainConsoleTest.Logger;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());

var programLogger = loggerFactory.CreateLogger<Program>();
var logger = loggerFactory.CreateLogger<CompanyLogger>();
var adapter = new CompanyLogger(logger);

var company = new Company(Guid.NewGuid(), "New company");
company.SetLogger(adapter);
var department1 = new Department(Guid.NewGuid(), "Department #1", company, new List<BaseEmployeeCommand>()
{
    new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7)),
    new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7)),
    new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7))
});
var department2 = new Department(Guid.NewGuid(), "Department #2", company, new List<BaseEmployeeCommand>()
{
    new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7)),
    new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7)),
    new EmployeeCommand(Guid.NewGuid(), Random.Shared.Next(4, 7))
});
department1.SetLogger(adapter);
department2.SetLogger(adapter);
company.AddDepartment(department1);
company.AddDepartment(department2);

var clientProj1 = new ClientProject(Guid.NewGuid(), "Project #1",
    14400, DateOnly.FromDateTime(DateTime.Now.AddDays(14)));
var clientProj2 = new ClientProject(Guid.NewGuid(), "Project #2",
    18500, DateOnly.FromDateTime(DateTime.Now.AddDays(21)));

BaseClient client = new Client(company, Guid.NewGuid(), "Test user #1", 120000);
var id1 = client.OrderProject(clientProj1);
var id2 =client.OrderProject(clientProj2);

var thread1 = new Thread(() => StartProcess(id1, company, programLogger));
var thread2 = new Thread(() => StartProcess(id2, company, programLogger));

thread1.Start();
thread2.Start();

Console.ReadKey();

LogCompany(company, programLogger);

void StartProcess(Guid projectId, BaseCompany company, ILogger<Program> logger)
{
    try
    {
        company.StartWorkOnProject(projectId);
    }
    catch (Exception e)
    {
        logger.LogError(e, "Exception");
    }
}

void LogCompany(Company company, ILogger<Program> logger)
{
    foreach (var item in company.GetAllProjects())
    {
        logger.LogInformation($"Id: {item.Id}; Name: {item.Title}; Status: {item.Status}");
    }
}