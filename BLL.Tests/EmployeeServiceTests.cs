using AutoMapper;
using BLL.DTOs;
using BLL.Security.Identity;
using BLL.Services.Impl;
using BLL.Services.Interfaces;
using DAL.Enums;
using DAL.UnitOfWork;
using Moq;
using Assert = Xunit.Assert;

namespace BLL.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void Ctor_InputNull_ThrowArgumentNullException()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;
        var mockedMapper = new Mock<IMapper>();
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new RequestService(nullUnitOfWork, mockedMapper.Object));
    }
    
    [Fact]
    public void GetEmployees_UserIsAdministrator_ThrowMethodAccessException()
    {
        // Arrange
        var user = new User(1, new List<Role>(){Role.Administrator});
        SecurityContext.SetUser(user);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockedMapper = new Mock<IMapper>();
        IEmployeeService employeeService = new EmployeeService(mockUnitOfWork.Object, mockedMapper.Object);

        // Act
        var actualGetOrdersFunc = () => employeeService.GetEmployeesFiltered(10);
        var exception = Record.Exception(actualGetOrdersFunc);

        // Assert
        Assert.IsNotType<MethodAccessException>(exception);
    }

    [Fact]
    public void GetEmployees_EmployeeFromDAL_CorrectMappingToOrderDTO()
    {
        // Arrange
        User user = new User(1, new List<Role>{ Role.Administrator });
        SecurityContext.SetUser(user);

        var expectedEmployeeDto = new EmployeeDto()
        {
            Id = 1,
            Name = "Denis",
            Surname = "Shapovalov",
            Email = "shapovalov.denis@lll.kpi.ua",
            Phone = "0957188244"
        };
        
        var employeerServiceFake = new EmployeeServiceFake(expectedEmployeeDto);
        var actualService = employeerServiceFake.Get();

        // Act
        var actualEmployeeDto = actualService.GetEmployeesFiltered(0).First();

        // Assert
        Assert.True(
            actualEmployeeDto.Id == expectedEmployeeDto.Id &&
            actualEmployeeDto.Name == expectedEmployeeDto.Name &&
            actualEmployeeDto.Surname == expectedEmployeeDto.Surname &&
            actualEmployeeDto.Email == expectedEmployeeDto.Email &&
            actualEmployeeDto.Phone == expectedEmployeeDto.Phone
        );
    }
}