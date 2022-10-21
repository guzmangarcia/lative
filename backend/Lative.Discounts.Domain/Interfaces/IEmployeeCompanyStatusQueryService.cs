using System.Threading.Tasks;
using Lative.Discounts.Domain.Requests;

namespace Lative.Discounts.Domain.Interfaces;

public interface IEmployeeCompanyStatusQueryService
{
    Task<GetEmployeeCompanyStatusResponse> GetEmployeeCompanyStatus(GetEmployeeCompanyStatusRequest request = null);
}