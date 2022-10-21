using System.Threading.Tasks;
using Lative.Discounts.Domain.Requests;

namespace Lative.Discounts.Domain.Interfaces;

public interface IDiscountQueryService
{
    Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request = null);
}