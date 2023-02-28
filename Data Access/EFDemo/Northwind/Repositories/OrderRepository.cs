using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.Repositories;

public class OrderRepository : RepositoryBase<Order>
{
    public OrderRepository(NorthwindContext context) : base(context)
    {
    }

    public override IEnumerable<Order> GetAll()
    {
        return context.Orders
            .Include(o => o.OrderDetails)
            .ToList();
    }
}
