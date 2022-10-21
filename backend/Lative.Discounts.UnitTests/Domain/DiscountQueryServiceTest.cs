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
public class DiscountQueryServiceTest

{
    [SetUp]
    public void SetUp()
    {
    }

    public readonly IMapper _mapper = new MapperMock<Discount, Discounts.Domain.Models.Discount>()._mapper;

    public IDb GetDatabase(int count)
    {
        var mock = new Mock<IDb>();
        mock.Setup(s => s.GetDiscounts(It.IsAny<ulong>(), It.IsAny<int>()))
            .Returns(Task.FromResult(GetDiscount(count)));
        mock.Setup(s => s.GetDiscounts()).Returns(Task.FromResult(GetDiscount(count)));
        return mock.Object;
    }

    private List<Discount> GetDiscount(int count)
    {
        var employeesDiscounts = new List<Discount>();
        for (var NumberEmployee = 0; NumberEmployee < count; NumberEmployee++)
            employeesDiscounts.Add(createDiscount(NumberEmployee));

        return employeesDiscounts;
    }

    private Discount createDiscount(int NumberEmployee)
    {
        return new Discount
        {
            Id = (ulong)NumberEmployee,
            DiscountPercent = NumberEmployee + 1,
            EmployeeCompanyStatusId = (ulong)(NumberEmployee + 2),
            Seniority = NumberEmployee + 3
        };
    }

    private readonly GetDiscountRequest request = new();

    [TestCase(0, false)]
    [TestCase(1, false)]
    [TestCase(10, false)]
    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(10, true)]
    public async Task GetDiscount_DiscountQueryService_ReturnsElements(int count, bool useRequest)
    {
        var discountsResponse =
            await new DiscountQueryService(GetDatabase(count), _mapper).GetDiscount(useRequest ? request : null);
        Assert.AreEqual(count, discountsResponse.Discounts.Count);
    }

    [TestCase(1, true)]
    [TestCase(1, false)]
    public async Task GetDiscount_DiscountQueryService_ReturnsCheckData(int count, bool useRequest)
    {
        var discountsResponse =
            await new DiscountQueryService(GetDatabase(count), _mapper).GetDiscount(useRequest ? request : null);
        count = 0;
        var elementAt0 = discountsResponse.Discounts[count];

        Assert.AreEqual(count, elementAt0.Id);
        Assert.AreEqual(count + 1, elementAt0.DiscountPercent);
        Assert.AreEqual(count + 2, elementAt0.EmployeeCompanyStatusId);
        Assert.AreEqual(count + 3, elementAt0.Seniority);
    }
}