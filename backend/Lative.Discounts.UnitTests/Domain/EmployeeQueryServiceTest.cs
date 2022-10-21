using System;
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

public class EmployeeQueryServiceTest
{
    public readonly IMapper _mapper = new MapperMock<Employee, Discounts.Domain.Models.Employee>()._mapper;


    private readonly GetEmployeesRequest request = new() { Active = false };


    public IDb GetDatabase(uint count)
    {
        var mock = new Mock<IDb>();
        mock.Setup(s => s.GetEmployees(It.IsAny<ulong>(), It.IsAny<DateTime?>(), It.IsAny<bool>()))
            .Returns(Task.FromResult(GetEmployees(count)));
        mock.Setup(s => s.GetEmployees()).Returns(Task.FromResult(GetEmployees(count)));
        return mock.Object;
    }


    private List<Employee> GetEmployees(uint count)
    {
        var employeesGetEmployees = new List<Employee>();
        for (uint NumberEmployee = 0; NumberEmployee < count; NumberEmployee++)
            employeesGetEmployees.Add(createGetEmployee(NumberEmployee));

        return employeesGetEmployees;
    }

    private Employee createGetEmployee(uint numberEmployee)
    {
        return new Employee
        {
            Id = numberEmployee,
            CompanyStatusId = numberEmployee + 1,
            FirstName = "Name",
            LastName = "Surname",
            HireDate = new DateTime(),
            EndDate = numberEmployee % 2 == 0 ? null : new DateTime()
        };
    }

    [TestCase((uint)0, false)]
    [TestCase((uint)1, false)]
    [TestCase((uint)10, false)]
    [TestCase((uint)0, true)]
    [TestCase((uint)1, true)]
    [TestCase((uint)10, true)]
    public async Task GetEmployees_EmployeeQueryService_ReturnsElements(uint count, bool useRequest)
    {
        var getEmployeesResponse =
            await new EmployeeQueryService(GetDatabase(count), _mapper).GetEmployees(useRequest ? request : null);
        Assert.AreEqual(count, getEmployeesResponse.EmployeeList.Count);
    }


    [TestCase((uint)2, false)]
    [TestCase((uint)2, true)]
    public async Task GetEmployees_EmployeeQueryService_CheckData(uint count, bool useRequest)
    {
        var getEmployeesResponse =
            await new EmployeeQueryService(GetDatabase(count), _mapper).GetEmployees(useRequest ? request : null);
        count = 0;
        var elementAt0 = getEmployeesResponse.EmployeeList[(int)count];

        Assert.AreEqual(count, elementAt0.Id);
        Assert.AreEqual(count + 1, elementAt0.CompanyStatusId);
        Assert.AreEqual("Name", elementAt0.FirstName);
        Assert.AreEqual("Surname", elementAt0.LastName);
        Assert.AreEqual(new DateTime(), elementAt0.HireDate);
        Assert.AreEqual(null, elementAt0.EndDate);

        count++;

        elementAt0 = getEmployeesResponse.EmployeeList[(int)count];

        Assert.AreEqual(count, elementAt0.Id);
        Assert.AreEqual(count + 1, elementAt0.CompanyStatusId);
        Assert.AreEqual("Name", elementAt0.FirstName);
        Assert.AreEqual("Surname", elementAt0.LastName);
        Assert.AreEqual(new DateTime(), elementAt0.HireDate);
        Assert.AreEqual(new DateTime(), elementAt0.EndDate);
    }
}