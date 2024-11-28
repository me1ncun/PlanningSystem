using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Enums;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Software_Development_Technologies.Controllers;

[ApiController]
[Route("api/v1/")]
public class RequestController : ControllerBase
{
    private readonly ILogger<RequestController> _logger;
    private readonly IRequestService _service;

    public RequestController(
        ILogger<RequestController> logger,
        IRequestService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("requests")]
    public async Task<IActionResult> GetAll()
    {
        var requests = await _service.GetAllRequestsAsync();

        return Ok(requests);
    }

    [HttpGet("requests/status/{id}")]
    public async Task<IActionResult> GetStatusById(int id)
    {
        try
        {
            var status = await _service.GetRequestStatusAsync(id);

            return Ok(status);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest();
        }
    }

    [HttpPost("requests")]
    public async Task<IActionResult> CreateRequest(RequestDto requestDto)
    {
        try
        {
            await _service.CreateRequestAsync(requestDto);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest();
        }
    }

    [HttpPut("requests")]
    public async Task<IActionResult> EditRequest(RequestDto requestDto)
    {
        try
        {
            await _service.UpdateRequestAsync(requestDto);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest();
        }
    }

    [HttpPut("requests/status")]
    public async Task<IActionResult> ChangeStatus(int id, RequestStatus requestStatus)
    {
        try
        {
            await _service.ChangeRequestStatusAsync(id, requestStatus);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest();
        }
    }

    [HttpDelete("requests/{id}")]
    public async Task<IActionResult> DeleteRequest(int id)
    {
        try
        {
            await _service.DeleteRequestAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            
            return BadRequest(ex.Message);
        }
    }
}