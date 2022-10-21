using System;
using System.Linq;
using System.Threading.Tasks;
using Lative.Discounts.Domain.Domain;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Requests;
using Lative.Discounts.UnitTests.Helpers;
using Moq;
using NUnit.Framework;

namespace Lative.Discounts.UnitTests.Domain;

[TestFixture]
public class EmployeeDiscountQueryServiceTest : EmployeeDiscountQueryServiceDataGenerator
{
    public readonly DateTime _time = new(2022, 10, 5);

    public IEmployeeCompanyStatusQueryService GetEmployeeCompanyStatusQueryService()
    {
        var mock = new Mock<IEmployeeCompanyStatusQueryService>();
        mock.Setup(s => s.GetEmployeeCompanyStatus(It.IsAny<GetEmployeeCompanyStatusRequest>())).Returns(
            Task.FromResult(new GetEmployeeCompanyStatusResponse
            {
                EmployeeCompanyStatuses = _employeeCompanyStatus
            }));

        return mock.Object;
    }

    public IDiscountQueryService GetDiscountQueryService()
    {
        var mock = new Mock<IDiscountQueryService>();
        mock.Setup(s => s.GetDiscount(It.IsAny<GetDiscountRequest>()))
            .Returns(Task.FromResult(new GetDiscountResponse { Discounts = _Discounts }));

        return mock.Object;
    }

    public IEmployeeQueryService GetEmployeeQueryService()
    {
        var mock = new Mock<IEmployeeQueryService>();
        mock.Setup(s => s.GetEmployees(It.IsAny<GetEmployeesRequest>())).Returns<GetEmployeesRequest>(request =>
            Task.FromResult(
                new GetEmployeesResponse
                {
                    EmployeeList = request == null || request?.CompanyStatusId == ulong.MaxValue
                        ? _employees
                        : _employees.Where(employee => employee.CompanyStatusId == request?.CompanyStatusId).ToList()
                }));

        return mock.Object;
    }


    private IEmployeeDiscountQueryService GetEmployeeDiscountService()
    {
        return new EmployeeDiscountQueryService(
            GetEmployeeCompanyStatusQueryService(),
            GetDiscountQueryService(),
            GetEmployeeQueryService(),
            new DateTimeProviderMock(_time));
    }


    [TestCase("requestPermanent")]
    [TestCase("requestPartTime")]
    [TestCase("requestIntern")]
    [TestCase("requestContractor")]
    [TestCase("None")]
    public async Task GetEmployeeDiscount_EmployeeCompanyStatusQueryService_ReturnsCountPermanent(string employeeType)
    {
        GetEmployeeDiscountRequest request = null;
        var count = 0;
        switch (employeeType)
        {
            case "requestPermanent":
                request = _requestPermanent;
                count = 15;
                break;
            case "requestPartTime":
                request = _requestPartTime;
                count = 6;
                break;
            case "requestIntern":
                request = _requestIntern;
                count = 2;
                break;
            case "requestContractor":
                request = _requestContractor;
                count = 2;
                break;
            case "None":
                request = null;
                count = 25;
                break;
            default:
                Assert.True(false);
                break;
        }

        var getEmployeeDiscountResponse = await GetEmployeeDiscountService().GetEmployeeDiscount(request);
        Assert.AreEqual(count, getEmployeeDiscountResponse.EmployeesDiscounts.Count);
    }


    [TestCase(true)]
    [TestCase(false)]
    public async Task GetEmployeeDiscount_EmployeeCompanyStatusQueryService_ReturnsSelectedCorrectDiscountsForPermanent(
        bool useRequest)
    {
        var getEmployeeDiscountResponse =
            await GetEmployeeDiscountService().GetEmployeeDiscount(useRequest ? _requestPermanent : null);

        Assert.AreEqual(15,
            getEmployeeDiscountResponse.EmployeesDiscounts
                .First(d => d.FirstName == "James" && d.LastName == "Smith" && d.Type == "Permanent").Discount);
        Assert.AreEqual(10,
            getEmployeeDiscountResponse.EmployeesDiscounts
                .First(d => d.FirstName == "William" && d.LastName == "Taylor" && d.Type == "Permanent").Discount);
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task GetEmployeeDiscount_EmployeeCompanyStatusQueryService_ReturnsSelectedCorrectDiscountsForPart_Time(
        bool useRequest)
    {
        var getEmployeeDiscountResponse =
            await GetEmployeeDiscountService().GetEmployeeDiscount(useRequest ? _requestPartTime : null);

        Assert.AreEqual(5,
            getEmployeeDiscountResponse.EmployeesDiscounts
                .First(d => d.FirstName == "Robert" && d.LastName == "Jones" && d.Type == "Part-time").Discount);
        Assert.AreEqual(8,
            getEmployeeDiscountResponse.EmployeesDiscounts
                .First(d => d.FirstName == "Thomas" && d.LastName == "Martin" && d.Type == "Part-time").Discount);
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task GetEmployeeDiscount_EmployeeCompanyStatusQueryService_ReturnsSelectedCorrectDiscountsForIntern(
        bool useRequest)
    {
        var getEmployeeDiscountResponse =
            await GetEmployeeDiscountService().GetEmployeeDiscount(useRequest ? _requestIntern : null);

        Assert.AreEqual(5,
            getEmployeeDiscountResponse.EmployeesDiscounts
                .First(d => d.FirstName == "Richard" && d.LastName == "Morton" && d.Type == "Intern").Discount);
    }


    [TestCase(true)]
    [TestCase(false)]
    public async Task
        GetEmployeeDiscount_EmployeeCompanyStatusQueryService_ReturnsSelectedCorrectDiscountsForContractor(
            bool useRequest)
    {
        var getEmployeeDiscountResponse =
            await GetEmployeeDiscountService().GetEmployeeDiscount(useRequest ? _requestContractor : null);

        Assert.AreEqual(0,
            getEmployeeDiscountResponse.EmployeesDiscounts
                .First(d => d.FirstName == "Mark" && d.LastName == "Ryan" && d.Type == "Contractor").Discount);
    }
}