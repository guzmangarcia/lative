using System;

namespace Lative.Discounts.Domain.Requests;

public class GetEmployeesRequest
{
    public GetEmployeesRequest()
    {
        Active = true;
        CompanyStatusId = ulong.MaxValue;
        HiredDate = null;
    }

    public ulong CompanyStatusId { get; set; }
    public DateTime? HiredDate { get; set; }

    public bool Active { get; set; }
}