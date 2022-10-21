using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lative.Discounts.Domain.Domain;
using Lative.Discounts.Domain.Requests;
using Lative.Discounts.Infrastructure;
using Lative.Discounts.Infrastructure.POCO;
using Moq;
using NUnit.Framework;

namespace Lative.Discounts.UnitTests.Domain;

[TestFixture]
public class EmployeeCompanyStatusQueryServiceTest
{
    [SetUp]
    public void SetUp()
    {
    }


    public readonly IMapper _mapper =
        new MapperMock<EmployeeCompanyStatus, Discounts.Domain.Models.EmployeeCompanyStatus>()._mapper;


    public IDb GetDatabase(int count)
    {
        var mock = new Mock<IDb>();
        mock.Setup(s => s.GetEmployeeCompanyStatus(It.IsAny<uint>()))
            .Returns(Task.FromResult(GetEmployeeCompanyStatus(count)));
        mock.Setup(s => s.GetEmployeeCompanyStatus()).Returns(Task.FromResult(GetEmployeeCompanyStatus(count)));
        return mock.Object;
    }


    private List<EmployeeCompanyStatus> GetEmployeeCompanyStatus(int count)
    {
        var employeesEmployeeCompanyStatuss = new List<EmployeeCompanyStatus>();
        for (var NumberEmployee = 0; NumberEmployee < count; NumberEmployee++)
            employeesEmployeeCompanyStatuss.Add(createEmployeeCompanyStatus(NumberEmployee));

        return employeesEmployeeCompanyStatuss;
    }

    private EmployeeCompanyStatus createEmployeeCompanyStatus(int NumberEmployee)
    {
        return new EmployeeCompanyStatus
        {
            Id = (ulong)NumberEmployee,
            CompanyStatus = "status"
        };
    }


    private readonly GetEmployeeCompanyStatusRequest request = new();

    [TestCase(0, false)]
    [TestCase(1, false)]
    [TestCase(10, false)]
    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(10, true)]
    public async Task GetEmployeeCompanyStatus_EmployeeCompanyStatusQueryService_ReturnsElements(int count,
        bool useRequest)
    {
        var EmployeeCompanyStatussResponse =
            await new EmployeeCompanyStatusQueryService(GetDatabase(count), _mapper).GetEmployeeCompanyStatus(
                useRequest ? request : null);
        Assert.AreEqual(count, EmployeeCompanyStatussResponse.EmployeeCompanyStatuses.Count);
    }


    [TestCase(1, true)]
    [TestCase(1, false)]
    public async Task GetEmployeeCompanyStatus_EmployeeCompanyStatusQueryService_ReturnsCheckData(int count,
        bool useRequest)
    {
        var EmployeeCompanyStatussResponse =
            await new EmployeeCompanyStatusQueryService(GetDatabase(count), _mapper).GetEmployeeCompanyStatus(
                useRequest ? request : null);
        count = 0;
        var elementAt0 = EmployeeCompanyStatussResponse.EmployeeCompanyStatuses[count];

        Assert.AreEqual(count, elementAt0.Id);
        Assert.AreEqual("status", elementAt0.CompanyStatus);
    }
}