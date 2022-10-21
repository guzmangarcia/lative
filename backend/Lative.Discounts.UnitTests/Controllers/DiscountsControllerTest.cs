using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lative.Discounts.API.Discounts;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Models;
using Lative.Discounts.Domain.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Lative.Discounts.UnitTests.Controllers;

[TestFixture]
public class DiscountsControllerTest
{
    [SetUp]
    public void SetUp()
    {
    }


    public readonly ILogger<DiscountsController> _ilogger = new LoggerMock<DiscountsController>()._ilogger;

    private IEmployeeDiscountQueryService GetEmployeeDiscountQueryService(int count)
    {
        var mock = new Mock<IEmployeeDiscountQueryService>();
        mock.Setup(s => s.GetEmployeeDiscount(It.IsAny<GetEmployeeDiscountRequest>()))
            .Returns(Task.FromResult(GetEmpoyeesDiscount(count)));
        return mock.Object;
    }

    private GetEmployeeDiscountResponse GetEmpoyeesDiscount(int count)
    {
        var employeesDiscounts = new List<EmployeeDiscount>();
        for (var NumberEmployee = 0; NumberEmployee < count; NumberEmployee++)
            employeesDiscounts.Add(createEmployeeDiscount(NumberEmployee));

        return new GetEmployeeDiscountResponse { EmployeesDiscounts = employeesDiscounts };
    }

    private EmployeeDiscount createEmployeeDiscount(int NumberEmployee)
    {
        return new EmployeeDiscount
        {
            Type = "Permanent",
            Discount = NumberEmployee,
            FirstName = "Name",
            LastName = "Surname"
        };
    }


    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public async Task GetAllEmployeesDiscounts_DiscountsController_ReturnsALLElements(int count)
    {
        var discountsResponse = await new DiscountsController(
            _ilogger,
            GetEmployeeDiscountQueryService(count)).GetAllEmployeesDiscounts();

        Assert.AreEqual(count, discountsResponse.EmployeesDiscounts.Count);
    }


    [TestCase(1)]
    public async Task GetAllEmployeesDiscounts_DiscountsController_ReturnsDataData(int count)
    {
        var discountsResponse = await new DiscountsController(
            _ilogger,
            GetEmployeeDiscountQueryService(count)).GetAllEmployeesDiscounts();

        count = 0;
        var elementAt0 = discountsResponse.EmployeesDiscounts[count];
        Assert.AreEqual("Permanent", elementAt0.Type);
        Assert.AreEqual(count, elementAt0.Discount);
        Assert.AreEqual("Name", elementAt0.FirstName);
        Assert.AreEqual("Surname", elementAt0.LastName);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public async Task GetEmployeesDiscounts_DiscountsController_ReturnsElementsDate(int count)
    {
        var discountsResponse = await new DiscountsController(
            _ilogger,
            GetEmployeeDiscountQueryService(count)).GetEmployeesDiscounts(new DateTime());

        Assert.AreEqual(count, discountsResponse.EmployeesDiscounts.Count);
    }


    [TestCase(1)]
    public async Task GetEmployeesDiscounts_DiscountsController_ReturnsCheckDataDate(int count)
    {
        var discountsResponse = await new DiscountsController(
            _ilogger,
            GetEmployeeDiscountQueryService(count)).GetEmployeesDiscounts(new DateTime());

        count = 0;
        var elementAt0 = discountsResponse.EmployeesDiscounts[count];
        Assert.AreEqual("Permanent", elementAt0.Type);
        Assert.AreEqual(count, elementAt0.Discount);
        Assert.AreEqual("Name", elementAt0.FirstName);
        Assert.AreEqual("Surname", elementAt0.LastName);
    }


    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public async Task GetEmployeesDiscounts_DiscountsController_ReturnsElementsDateType(int count)
    {
        var discountsResponse = await new DiscountsController(
            _ilogger,
            GetEmployeeDiscountQueryService(count)).GetEmployeesDiscounts(new DateTime(), "");

        Assert.AreEqual(count, discountsResponse.EmployeesDiscounts.Count);
    }


    [TestCase(1)]
    public async Task GetEmployeesDiscounts_DiscountsController_ReturnsCheckDataDateType(int count)
    {
        var discountsResponse = await new DiscountsController(
            _ilogger,
            GetEmployeeDiscountQueryService(count)).GetEmployeesDiscounts(new DateTime(), "");

        count = 0;
        var elementAt0 = discountsResponse.EmployeesDiscounts[count];
        Assert.AreEqual("Permanent", elementAt0.Type);
        Assert.AreEqual(count, elementAt0.Discount);
        Assert.AreEqual("Name", elementAt0.FirstName);
        Assert.AreEqual("Surname", elementAt0.LastName);
    }
}