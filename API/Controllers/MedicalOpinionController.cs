using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Software_Development_Technologies.Controllers;

[ApiController]
[Route("api/v1/")]
public class MedicalOpinionController : ControllerBase
{
    private readonly IMedicalOpinionService _service;
    private readonly ILogger<MedicalOpinionController> _logger;

    public MedicalOpinionController(
        IMedicalOpinionService service,
        ILogger<MedicalOpinionController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("medicalOpinions")]
    public async Task<IActionResult> GetMedicalOpinions()
    {
        var medicalOpinions = await _service.GetMedicalOpinionsAsync();

        return Ok(medicalOpinions);
    }

    [HttpGet("medicalOpinions/{id}")]
    public async Task<IActionResult> GetMedicalOpinion(int id)
    {
        try
        {
            var medicalOpinion = await _service.GetMedicalOpinionById(id);

            return Ok(medicalOpinion);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpPost("medicalOpinions")]
    public async Task<IActionResult> CreateMedicalOpinion(MedicalOpinionDto medicalOpinionDto)
    {
        try
        {
            await _service.CreateMedicalOpinion(medicalOpinionDto);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpPut("medicalOpinions")]
    public async Task<IActionResult> EditMedicalOpinion(MedicalOpinionDto medicalOpinionDto)
    {
        try
        {
            await _service.EditMedicalOpinion(medicalOpinionDto);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("medicalOpinions/{id}")]
    public async Task<IActionResult> DeleteMedicalOpinion(int id)
    {
        try
        {
            await _service.DeleteMedicalOpinion(id);

            return Ok();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);
            
            return BadRequest(ex.Message);
        }
    }
}