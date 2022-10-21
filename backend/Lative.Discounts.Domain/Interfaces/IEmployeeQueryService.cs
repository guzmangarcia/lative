using System.Threading.Tasks;
using Lative.Discounts.Domain.Requests;

namespace Lative.Discounts.Domain.Interfaces;

public interface IEmployeeQueryService
{
    Task<GetEmployeesResponse> GetEmployees(GetEmployeesRequest request = null);
}