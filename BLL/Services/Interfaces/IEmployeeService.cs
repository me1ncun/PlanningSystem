using BLL.DTOs;

namespace BLL.Services.Interfaces;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployeesFiltered(int pageNumber);
    Task<EmployeeDto> GetEmployeeByIdAsync(int id);
    Task CreateEmployeeAsync(EmployeeDto employeeDto);
}