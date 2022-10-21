using System.Threading.Tasks;
using Lative.Discounts.Domain.Requests;

namespace Lative.Discounts.Domain.Interfaces;

public interface IEmployeeDiscountQueryService
{
    Task<GetEmployeeDiscountResponse> GetEmployeeDiscount(GetEmployeeDiscountRequest request = null);
}