using System;

namespace Lative.Discounts.Domain.Utils;

//Implementation with real DateTime.Now
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetNow()
    {
        return DateTime.Now;
    }
}