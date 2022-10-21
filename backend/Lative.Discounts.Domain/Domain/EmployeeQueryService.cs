using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Requests;
using Lative.Discounts.Infrastructure;
using Lative.Discounts.Infrastructure.POCO;

namespace Lative.Discounts.Domain.Domain;

public class EmployeeQueryService : GeneralService, IEmployeeQueryService
{
    public EmployeeQueryService(IDb database, IMapper mapper) : base(database, mapper)
    {
    }


    public async Task<GetEmployeesResponse> GetEmployees(GetEmployeesRequest request = null)
    {
        List<Employee> employees = null;


        if (request == null)
            employees = await _database.GetEmployees();
        else
            employees = await _database.GetEmployees(request.CompanyStatusId, request.HiredDate, request.Active);


        return Map(employees);
    }

    private GetEmployeesResponse Map(List<Employee> result)
    {
        return new GetEmployeesResponse
            { EmployeeList = result.Select(employee => _mapper.Map<Models.Employee>(employee)).ToList() };
    }
}