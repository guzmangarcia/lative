using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lative.Discounts.Infrastructure.POCO;

namespace Lative.Discounts.Infrastructure;

public class Db : IDb
{
    private readonly List<Discount> _discounts = new()
    {
        new Discount()
        {
            Id = 0,
            DiscountPercent = 10,
            EmployeeCompanyStatusId = 0, //"Permanent"
            Seniority = 0
        },
        new Discount()
        {
            Id = 1,
            DiscountPercent = 5,
            EmployeeCompanyStatusId = 0, //"Permanent"
            Seniority = 3
        },
        new Discount()
        {
            Id = 2,
            DiscountPercent = 5,
            EmployeeCompanyStatusId = 1, //"Part-time"
            Seniority = 0
        },
        new Discount()
        {
            Id = 3,
            DiscountPercent = 3,
            EmployeeCompanyStatusId = 1, //"Part-time"
            Seniority = 5
        },
        new Discount()
        {
            Id = 4,
            DiscountPercent = 5,
            EmployeeCompanyStatusId = 2, //"Intern"
            Seniority = 0
        },
        new Discount()
        {
            Id = 5,
            DiscountPercent = 0,
            EmployeeCompanyStatusId = 3, //"Contractor"
            Seniority = 0
        }
    };

    protected readonly List<EmployeeCompanyStatus> _employeeCompanyStatus = new()
    {
        new EmployeeCompanyStatus()
        {
            Id = 0,
            CompanyStatus = "Permanent"
        },
        new EmployeeCompanyStatus()
        {
            Id = 1,
            CompanyStatus = "Part-time"
        },
        new EmployeeCompanyStatus()
        {
            Id = 2,
            CompanyStatus = "Intern"
        },
        new EmployeeCompanyStatus()
        {
            Id = 3,
            CompanyStatus = "Contractor"
        }
    };

    protected readonly List<Employee> _employees = new()
    {
        new()
        {
            Id = 1,
            FirstName = "James",
            LastName = "Smith",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 2,
            FirstName = "Robert",
            LastName = "Jones",
            CompanyStatusId = 1,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Brown",
            CompanyStatusId = 2,
            HireDate = new DateTime(2020, 1, 1)
        },
        new()
        {
            Id = 4,
            FirstName = "David",
            LastName = "Wilson",
            CompanyStatusId = 1,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 5,
            FirstName = "William",
            LastName = "Taylor",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },
        new()
        {
            Id = 6,
            FirstName = "Richard",
            LastName = "Morton",
            CompanyStatusId = 2,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 7,
            FirstName = "Joseph",
            LastName = "White",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },

        new()
        {
            Id = 8,
            FirstName = "Charles",
            LastName = "Anderson",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },
        new()
        {
            Id = 9,
            FirstName = "Christopher",
            LastName = "Anderson",
            CompanyStatusId = 1,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 10,
            FirstName = "Daniel",
            LastName = "Wang",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },
        new()
        {
            Id = 11,
            FirstName = "Matthew",
            LastName = "Li",
            CompanyStatusId = 1,
            HireDate = new DateTime(2010, 1, 1)
        },
        new()
        {
            Id = 12,
            FirstName = "Anthony",
            LastName = "Rodriguez",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },
        new()
        {
            Id = 13,
            FirstName = "Mark",
            LastName = "Ryan",
            CompanyStatusId = 3,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 14,
            FirstName = "Donald",
            LastName = "Gelbero",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },
        new()
        {
            Id = 15,
            FirstName = "Steven",
            LastName = "Tremblay",
            CompanyStatusId = 1,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 16,
            FirstName = "Paul",
            LastName = "Gagnon",
            CompanyStatusId = 3,
            HireDate = new DateTime(2022, 1, 1)
        },
        new()
        {
            Id = 17,
            FirstName = "Andrew",
            LastName = "Evans",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 18,
            FirstName = "Joshua",
            LastName = "Davies",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 19,
            FirstName = "Kenneth",
            LastName = "Sullivan",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 20,
            FirstName = "Kevin",
            LastName = "Rodriguez",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 21,
            FirstName = "Brian",
            LastName = "Wilson",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 22,
            FirstName = "George",
            LastName = "Gomez",
            CompanyStatusId = 0,
            HireDate = new DateTime(2018, 1, 1)
        },
        new()
        {
            Id = 23,
            FirstName = "John",
            LastName = "Williams",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },
        new()
        {
            Id = 24,
            FirstName = "Tohn",
            LastName = "Filliams",
            CompanyStatusId = 0,
            HireDate = new DateTime(2021, 1, 1) //10
        },

        new()
        {
            Id = 25,
            FirstName = "Thomas",
            LastName = "Martin",
            CompanyStatusId = 1,
            HireDate = new DateTime(2010, 1, 1)
        }
    };

    public async Task<List<Discount>> GetDiscounts(ulong employeeCompanyStatusId, int seniority)
    {
        await Task.Delay(100);
        return _discounts.Where(discount =>
            discount.EmployeeCompanyStatusId == employeeCompanyStatusId &&
            discount.Seniority <= seniority).ToList();
    }

    public async Task<List<Employee>> GetEmployees()
    {
        await Task.Delay(100);
        return _employees;
    }

    public async Task<List<Employee>> GetEmployees(ulong companyStatusId, DateTime? hireDate, bool active)
    {
        await Task.Delay(100);

        hireDate = hireDate ?? DateTime.Now;
        var query = _employees.Where(employee =>
            employee.HireDate <= hireDate && employee.CompanyStatusId == companyStatusId);
        if (active) query = query.Where(employee => employee.EndDate == null);

        return query.OrderBy(d => d.FirstName).ThenBy(d => d.LastName).ToList();
    }

    public async Task<List<EmployeeCompanyStatus>> GetEmployeeCompanyStatus(uint id)
    {
        await Task.Delay(100);
        return _employeeCompanyStatus.Where(discount => discount.Id == id).ToList();
    }

    public async Task<List<Discount>> GetDiscounts()
    {
        await Task.Delay(100);
        return _discounts.ToList();
    }

    public async Task<List<EmployeeCompanyStatus>> GetEmployeeCompanyStatus()
    {
        await Task.Delay(100);
        return _employeeCompanyStatus.ToList();
    }
}