using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lative.Discounts.Infrastructure.POCO;

namespace Lative.Discounts.Infrastructure;

public interface IDb
{
    Task<List<Discount>> GetDiscounts();
    Task<List<Discount>> GetDiscounts(ulong employeeCompanyStatusId, int seniority);
    Task<List<EmployeeCompanyStatus>> GetEmployeeCompanyStatus();
    Task<List<EmployeeCompanyStatus>> GetEmployeeCompanyStatus(uint id);
    Task<List<Employee>> GetEmployees();
    Task<List<Employee>> GetEmployees(ulong companyStatusId, DateTime? hiredEmployee, bool active);
}