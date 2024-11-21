using AutoMapper;
using BLL.DTOs;
using BLL.Security.Identity;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;

namespace BLL.Services.Impl;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Employee> _repository;
    private readonly IMapper _mapper;
    private int pageSize = 10;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<Employee>();
        _mapper = mapper;
    }

    /// <exception cref="MethodAccessException"></exception>
    public IEnumerable<EmployeeDto> GetEmployeesFiltered(int pageNumber)
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();
        if (userType is null && !user.Roles.Contains(Role.Employee))
        {
            throw new MethodAccessException();
        }

        var employeeId = user.UserId;
        var employees = _unitOfWork.Employees.Find(e => e.Id == employeeId, pageNumber, pageSize);

        var employeesDto = _mapper.Map<List<EmployeeDto>>(employees);
        return employeesDto;
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();
        if (userType is null && !user.Roles.Contains(Role.Employee))
        {
            throw new MethodAccessException();
        }

        var employeeId = user.UserId;
        var employee = await _repository.GetById(employeeId);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public async Task CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        await _repository.Create(employee);
        await _unitOfWork.SaveChanges();

        User user = new User(employee.Id, new List<Role>() { Role.Employee });
        SecurityContext.SetUser(user);
    }
}