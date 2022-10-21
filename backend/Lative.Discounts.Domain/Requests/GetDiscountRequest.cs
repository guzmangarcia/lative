namespace Lative.Discounts.Domain.Requests;

public class GetDiscountRequest
{
    public ulong EmployeeCompanyStatusId { get; set; }
    public int Seniority { get; set; }
}