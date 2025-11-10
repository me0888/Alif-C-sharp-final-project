using Microsoft.AspNetCore.Mvc;

using Erm.PresentationLayer.WebApi.Authorization;
using Erm.BusinessLayer.Service;
using Erm.BusinessLayer.DTO;

namespace Erm.PresentationLayer.WebApi.Controllers;

[ApiController]
[Route("api/riskprofiles")]
public sealed class RiskProfileController(
    IRiskProfileService riskProfileService,
    ILogger<RiskProfileController> logger) : ControllerBase
{
    private readonly IRiskProfileService _riskProfileService = riskProfileService;
    private readonly ILogger<RiskProfileController> _logger = logger;

    [HttpPost]
    [ApiKey]
    public async Task<IActionResult> Create([FromBody] RiskProfileCreateDTO riskProfileDTO)
    {
        try
        {
            var riskProfile = await _riskProfileService.CreateAsync(riskProfileDTO);
            return CreatedAtAction(nameof(Get), new { id = riskProfile.Id }, riskProfile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
	
	[HttpGet]
    [ApiKey]
	[Route("all")]
    public async Task<IActionResult> GetAll()
        => Ok(await _riskProfileService.GetAllAsync());

    [HttpGet]
    [ApiKey]
    public async Task<IActionResult> Query([FromQuery] string? query)
        => !string.IsNullOrEmpty(query) ? Ok(await _riskProfileService.QueryAsync(query)) : BadRequest();

    [HttpGet]
    [ApiKey]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            return Ok(await _riskProfileService.GetAsync(id));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpPut]
    [ApiKey]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RiskProfileCreateDTO riskProfileDTO)
    {
        try
        {
            return Ok(await _riskProfileService.UpdateAsync(id, riskProfileDTO));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpPatch]
    [ApiKey]
    [Route("{id}")]
    public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] RiskProfileUpdateDTO riskProfileDTO)
    {
        try
        {
            return Ok(await _riskProfileService.UpdatePatchAsync(id, riskProfileDTO));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }



    [HttpDelete]
    [ApiKey]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _riskProfileService.DeleteAsync(id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
        return Ok();
    }
}