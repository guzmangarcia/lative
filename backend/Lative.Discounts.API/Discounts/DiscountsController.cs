using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lative.Discounts.API.Discounts;

[Route("api/get-discounts")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IEmployeeDiscountQueryService _employeeDiscountQueryService;

    private readonly ILogger<DiscountsController> _logger;

    public DiscountsController(
        ILogger<DiscountsController> logger,
        IEmployeeDiscountQueryService employeeDiscountQueryService
    )
    {
        _logger = logger;
        _employeeDiscountQueryService = employeeDiscountQueryService;
    }

    [HttpGet]
    [Route("")]
    public async Task<GetEmployeeDiscountResponse> GetAllEmployeesDiscounts()
    {
        return await _employeeDiscountQueryService.GetEmployeeDiscount();
    }


    [Route("{searchDate}")]
    [HttpGet]
    public async Task<GetEmployeeDiscountResponse> GetEmployeesDiscounts(DateTime searchDate)
    {
        return await _employeeDiscountQueryService.GetEmployeeDiscount(
            new GetEmployeeDiscountRequest { SearchDate = searchDate });
    }


    [Route("{searchDate}/{employeeType}")]
    [HttpGet]
    public async Task<GetEmployeeDiscountResponse> GetEmployeesDiscounts(DateTime searchDate, string employeeType)
    {
        return await _employeeDiscountQueryService.GetEmployeeDiscount(
            new GetEmployeeDiscountRequest { SearchDate = searchDate, EmployeeType = employeeType });
    }
}