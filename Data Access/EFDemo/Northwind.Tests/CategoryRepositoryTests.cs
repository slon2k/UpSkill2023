using Microsoft.EntityFrameworkCore;
using Moq;
using Northwind.Data;
using Northwind.Models;
using Northwind.Repositories;

namespace EFDemoApp.Domain.Tests;

public class CategoryRepositoryTests
{
    [Fact]
    public void GetAll_Returns_CategoryList()
    {
        var data = new List<Category>
        {
            new Category{ CategoryId = 1, CategoryName = "Category 1", Description = "Description 1" },
            new Category{ CategoryId = 2, CategoryName = "Category 2", Description = "Description 2" },
            new Category{ CategoryId = 3, CategoryName = "Category 3", Description = "Description 3" },
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Category>>();
        mockSet.As<IQueryable<Category>>().Setup(c => c.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Category>>().Setup(c => c.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Category>>().Setup(c => c.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Category>>().Setup(c => c.GetEnumerator()).Returns(data.GetEnumerator);

        var mockContext = new Mock<NorthwindContext>();
        mockContext.Setup(c => c.Categories).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

        var repo = new CategoryRepository(mockContext.Object);

        var result = repo.GetAll().ToList();

        Assert.Equal(3, result.Count);
        Assert.Equal("Category 1", result[0].CategoryName);
        Assert.Equal("Category 2", result[1].CategoryName);
        Assert.Equal("Category 3", result[2].CategoryName);
    }

    [Fact]
    public void Create_Adds_Category_To_Context()
    {
        var mockContext = new Mock<NorthwindContext>();

        var repo = new CategoryRepository(mockContext.Object);
        
        repo.Create(new Category { CategoryId = 1, CategoryName = "Category 1", Description = "Description 1" });

        mockContext.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);

        mockContext.Verify(x => x.Add(It.Is<Category>(c => c.CategoryName == "Category 1")), Times.Once);
        mockContext.Verify(x => x.Add(It.Is<Category>(c => c.Description == "Description 1")), Times.Once);
    }
}
