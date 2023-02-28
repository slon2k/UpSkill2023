using Northwind.Data;
using Northwind.Models;

namespace Northwind.Repositories;

public class CategoryRepository : RepositoryBase<Category>
{
    public CategoryRepository(NorthwindContext context) : base(context)
    {
    }
}
