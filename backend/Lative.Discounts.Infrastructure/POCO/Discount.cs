namespace Lative.Discounts.Infrastructure.POCO;

public class Discount
{
    public ulong Id { get; set; }

    public ulong EmployeeCompanyStatusId { get; set; }
    public int Seniority { get; set; }
    public int DiscountPercent { get; set; }
}