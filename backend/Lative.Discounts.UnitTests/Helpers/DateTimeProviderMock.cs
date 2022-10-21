using System;
using Lative.Discounts.Domain.Utils;

namespace Lative.Discounts.UnitTests.Helpers;

public class DateTimeProviderMock : IDateTimeProvider
{
    private readonly DateTime _fixedDateTime;

    public DateTimeProviderMock(DateTime fixedDateTime)
    {
        _fixedDateTime = fixedDateTime;
    }

    public DateTime GetNow()
    {
        return _fixedDateTime;
    }
}