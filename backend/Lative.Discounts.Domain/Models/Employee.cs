using System;

namespace Lative.Discounts.Domain.Models;

public class Employee
{
    public Employee()
    {
        EndDate = null;
    }

    public ulong Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    public ulong CompanyStatusId { get; set; }

    public DateTime HireDate { get; set; }

    public DateTime? EndDate { get; set; }
}