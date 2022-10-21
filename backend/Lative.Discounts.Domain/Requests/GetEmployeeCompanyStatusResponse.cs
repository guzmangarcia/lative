using System.Collections.Generic;
using Lative.Discounts.Domain.Models;

namespace Lative.Discounts.Domain.Requests;

public class GetEmployeeCompanyStatusResponse
{
    public List<EmployeeCompanyStatus> EmployeeCompanyStatuses { get; set; }
}