using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Models;
using Lative.Discounts.Domain.Requests;
using Lative.Discounts.Domain.Utils;

namespace Lative.Discounts.Domain.Domain;

public class EmployeeDiscountQueryService : IEmployeeDiscountQueryService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDiscountQueryService _discountQueryService;
    private readonly IEmployeeCompanyStatusQueryService _employeeCompanyStatusQueryService;
    private readonly IEmployeeQueryService _employeeQueryService;

    public EmployeeDiscountQueryService(
        IEmployeeCompanyStatusQueryService employeeCompanyStatusQueryService,
        IDiscountQueryService discountQueryService,
        IEmployeeQueryService employeeQueryService,
        IDateTimeProvider dateTimeProvider
    )
    {
        _employeeCompanyStatusQueryService = employeeCompanyStatusQueryService;
        _discountQueryService = discountQueryService;
        _employeeQueryService = employeeQueryService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<GetEmployeeDiscountResponse> GetEmployeeDiscount(GetEmployeeDiscountRequest request = null)
    {
        var now = _dateTimeProvider.GetNow();
        List<Employee> employees;
        var employeeStatus = (await _employeeCompanyStatusQueryService.GetEmployeeCompanyStatus())
            .EmployeeCompanyStatuses;
        var discounts = (await _discountQueryService.GetDiscount()).Discounts;


        if (request == null)
        {
            employees = (await _employeeQueryService.GetEmployees()).EmployeeList;
        }
        else
        {
            var employeeStatusId = FilterEmployeeStatus(request, employeeStatus);

            employees = (await _employeeQueryService.GetEmployees(new GetEmployeesRequest
                    { CompanyStatusId = employeeStatusId, HiredDate = request.SearchDate, Active = false }))
                .EmployeeList;
        }


        return Map(employees, employeeStatus, discounts, request?.SearchDate ?? now);
    }

    private static ulong FilterEmployeeStatus(
        GetEmployeeDiscountRequest request,
        List<EmployeeCompanyStatus> employeeStatus)
    {
        if (string.IsNullOrWhiteSpace(request.EmployeeType))
            throw new InvalidFilterCriteriaException("CompanyStatus is empty");

        employeeStatus = employeeStatus.Where(status => status.CompanyStatus == request.EmployeeType).ToList();

        if (employeeStatus.Count == 0) throw new InvalidFilterCriteriaException("CompanyStatus is invalid");

        return employeeStatus.First().Id;
    }

    private GetEmployeeDiscountResponse Map(
        List<Employee> employees,
        List<EmployeeCompanyStatus> employeeStatus,
        List<Discount> discounts,
        DateTime searchDate)
    {
        return new GetEmployeeDiscountResponse
        {
            EmployeesDiscounts = employees
                .Select(employee => new EmployeeDiscount
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Type = GetCompanyStatus(employee, employeeStatus),
                    Discount = GetTotalDiscount(employee, discounts, searchDate)
                }).OrderBy(d => d.FirstName).ThenBy(d => d.LastName).ToList()
        };
    }

    private static string GetCompanyStatus(
        Employee employee,
        List<EmployeeCompanyStatus> employeeStatus)
    {
        return employeeStatus.FirstOrDefault(status => status.Id == employee.CompanyStatusId)?.CompanyStatus ??
               "UKNOWN";
    }

    private static int GetTotalDiscount(
        Employee employee,
        List<Discount> discounts,
        DateTime now)
    {
        return discounts?.Where(discount =>
                discount.EmployeeCompanyStatusId == employee.CompanyStatusId
                && discount.Seniority <= now.Year - employee.HireDate.Year)
            ?.Select(d => d.DiscountPercent)
            ?.Aggregate((total, added) => total += added) ?? 0;
    }
}