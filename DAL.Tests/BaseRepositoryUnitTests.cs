using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using Assert = Xunit.Assert;

namespace DAL.Tests;

public class BaseRepositoryUnitTests
{
    [Fact]
    public async Task Create_InputEmployeeInstance_CalledAddMethodOfDBSetWithEmployeeInstance()
    {
        // Arrange
        DbContextOptions options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        var mockContext = new Mock<ApplicationDbContext>(options);
        var mockSet = new Mock<DbSet<Employee>>();
        mockContext
            .Setup(context => context.Set<Employee>())
            .Returns(mockSet.Object);
        var repository = new EmployeeRepository(mockContext.Object);
        Employee expectedEmployee = new Employee();
        
        // Act
        await repository.Create(expectedEmployee);

        // Assert
        mockSet.Verify(set => set.AddAsync(expectedEmployee, new CancellationToken()), Times.Once);
    }

    [Fact]
    public async Task Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
    {
        // Arrange
        DbContextOptions options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        var mockContext = new Mock<ApplicationDbContext>(options);
        var mockSet = new Mock<DbSet<Employee>>();
        mockContext
            .Setup(context => context.Set<Employee>())
            .Returns(mockSet.Object);
        var repository = new EmployeeRepository(mockContext.Object);
        Employee expectedEmployee = new Employee()
        {
            Id = 1
        };
        mockSet.Setup(mock => mock.FindAsync(expectedEmployee.Id))
            .ReturnsAsync(expectedEmployee);
        
        // Act
        var actualEmployee = await repository.GetById(expectedEmployee.Id);
        
        // Assert
        mockSet.Verify(dbset => dbset.FindAsync(expectedEmployee.Id), Times.Once);
        Assert.Equal(expectedEmployee.Id, actualEmployee.Id);
    }

    [Fact]
    public async Task Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
    {
        // Arrange
        DbContextOptions options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        var mockContext = new Mock<ApplicationDbContext>(options);
        var mockSet = new Mock<DbSet<Employee>>();
        mockContext
            .Setup(context => context.Set<Employee>())
            .Returns(mockSet.Object);
        var repository = new EmployeeRepository(mockContext.Object);
        Employee expectedEmployee = new Employee()
        {
            Id = 1
        };
        mockSet.Setup(mock => mock.FindAsync(expectedEmployee.Id))
            .ReturnsAsync(expectedEmployee);
        // Act
        await repository.Delete(expectedEmployee.Id);
        
        // Assert
        mockSet.Verify(dbset => dbset.FindAsync(expectedEmployee.Id), Times.Once);
        mockSet.Verify(dbset => dbset.Remove(expectedEmployee), Times.Once);
    }
}