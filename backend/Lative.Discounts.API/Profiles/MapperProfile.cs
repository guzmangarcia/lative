using AutoMapper;
using Lative.Discounts.Infrastructure.POCO;

namespace Lative.Discounts.API.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<Discount, Domain.Models.Discount>();
        CreateMap<Employee, Domain.Models.Employee>();
        CreateMap<EmployeeCompanyStatus, Domain.Models.EmployeeCompanyStatus>();
    }
}