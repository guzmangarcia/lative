using System.Collections.Generic;
using Lative.Discounts.Domain.Models;

namespace Lative.Discounts.Domain.Requests;

public class GetEmployeeDiscountResponse

{
    public List<EmployeeDiscount> EmployeesDiscounts { get; set; }
}