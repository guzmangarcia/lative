using AutoMapper;
using Lative.Discounts.Infrastructure;

namespace Lative.Discounts.Domain.Domain;

public class GeneralService
{
    protected readonly IDb _database;
    protected readonly IMapper _mapper;

    protected GeneralService(IDb database, IMapper mapper)
    {
        _mapper = mapper;
        _database = database;
    }
}