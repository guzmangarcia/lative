using AutoMapper;
using NUnit.Framework;

namespace Lative.Discounts.UnitTests;

[TestFixture]
public class MapperMock<T, K>
    where T : class
    where K : class
{
    public IMapper _mapper
    {
        get
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<T, K>()
            );
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}