using System.Collections.Generic;
using Lative.Discounts.Domain.Models;

namespace Lative.Discounts.Domain.Requests;

public class GetDiscountResponse
{
    public List<Discount> Discounts { get; set; }
}