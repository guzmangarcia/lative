using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Lative.Discounts.UnitTests;

[TestFixture]
public class LoggerMock<L> where L : class
{
    public ILogger<L> _ilogger => new Mock<ILogger<L>>().Object;
}