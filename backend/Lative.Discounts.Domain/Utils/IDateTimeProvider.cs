using System;

namespace Lative.Discounts.Domain.Utils;

//Interface
public interface IDateTimeProvider
{
    DateTime GetNow();
}