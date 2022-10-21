using System;

namespace Lative.Discounts.Domain.Requests;

public class GetEmployeeDiscountRequest
{
    public string EmployeeType { get; set; }

    public DateTime? SearchDate { get; set; }
}