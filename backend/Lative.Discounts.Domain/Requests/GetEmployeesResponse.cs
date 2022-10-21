using System.Collections.Generic;
using Lative.Discounts.Domain.Models;

namespace Lative.Discounts.Domain.Requests;

public class GetEmployeesResponse
{
    public List<Employee> EmployeeList { get; set; }
}