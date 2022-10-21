using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lative.Discounts.Domain.Interfaces;
using Lative.Discounts.Domain.Requests;
using Lative.Discounts.Infrastructure;
using Lative.Discounts.Infrastructure.POCO;

namespace Lative.Discounts.Domain.Domain;

public class EmployeeCompanyStatusQueryService : GeneralService, IEmployeeCompanyStatusQueryService
{
    public EmployeeCompanyStatusQueryService(IDb database, IMapper mapper) : base(database, mapper)
    {
    }

    public async Task<GetEmployeeCompanyStatusResponse> GetEmployeeCompanyStatus(
        GetEmployeeCompanyStatusRequest request = null)
    {
        List<EmployeeCompanyStatus> result = null;
        if (request == null)
            result = await _database.GetEmployeeCompanyStatus();
        else
            result = await _database.GetEmployeeCompanyStatus(request.Id);
        return Map(result);
    }

    private GetEmployeeCompanyStatusResponse Map(IEnumerable<EmployeeCompanyStatus> result)
    {
        return new GetEmployeeCompanyStatusResponse
        {
            EmployeeCompanyStatuses =
                result.Select(status => _mapper.Map<Models.EmployeeCompanyStatus>(status)).ToList()
        };
    }
}