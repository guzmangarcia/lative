using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Requests;
using Lative.Discounts.Infrastructure;
using Lative.Discounts.Infrastructure.POCO;

namespace Lative.Discounts.Domain.Domain;

public class DiscountQueryService : GeneralService, IDiscountQueryService
{
    public DiscountQueryService(IDb database, IMapper mapper) : base(database, mapper)
    {
    }


    public async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request = null)
    {
        List<Discount> result;
        if (request == null)
            result = await _database.GetDiscounts();
        else
            result = await _database.GetDiscounts(request.EmployeeCompanyStatusId, request.Seniority);
        return Map(result);
    }

    private GetDiscountResponse Map(IEnumerable<Discount> result)
    {
        return new GetDiscountResponse { Discounts = result.Select(emp => _mapper.Map<Models.Discount>(emp)).ToList() };
    }
}