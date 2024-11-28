using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Software_Development_Technologies.Controllers;

[ApiController]
[Route("api/v1/")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(
        IEmployeeService service,
        ILogger<EmployeeController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("employees/page/{number}")]
    public IActionResult GetAllEmployees(int number)
    {
        var employees = _service.GetEmployeesFiltered(number);

        return Ok(employees);
    }

    [HttpGet("employees/{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        try
        {
            var employee = await _service.GetEmployeeByIdAsync(id);

            return Ok(employee);
        }
        catch (MethodAccessException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("employees")]
    public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
    {
        try
        {
            await _service.CreateEmployeeAsync(employeeDto);

            return Ok();
        }
        catch (MethodAccessException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("employees/{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            await _service.DeleteEmployeeAsync(id);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }
}