using Microsoft.AspNetCore.Mvc;

using Erm.PresentationLayer.WebApi.Authorization;
using Erm.BusinessLayer.Service;
using Erm.BusinessLayer.DTO;

namespace Erm.PresentationLayer.WebApi.Controllers;

[ApiController]
[Route("api/risks")]
public sealed class RiskController(
    IRiskService riskService,
    ILogger<RiskController> logger) : ControllerBase
{
    private readonly IRiskService _riskService = riskService;
    private readonly ILogger<RiskController> _logger = logger;

    [HttpPost]
    [ApiKey]
    public async Task<IActionResult> Create([FromBody] RiskCreateDTO riskDTO)
    {
        try
        {
            var risk = await _riskService.CreateAsync(riskDTO);
            return CreatedAtAction(nameof(Get), new { id = risk.Id }, risk);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
	
	
	[HttpGet]
    [ApiKey]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _riskService.GetAllAsync());
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

    [HttpGet]
    [ApiKey]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            return Ok(await _riskService.GetAsync(id));
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
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RiskCreateDTO riskDTO)
    {
        try
        {
            return Ok(await _riskService.UpdateAsync(id, riskDTO));
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
    public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] RiskUpdateDTO riskDTO)
    {
        try
        {
            return Ok(await _riskService.UpdatePatchAsync(id, riskDTO));
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
            await _riskService.DeleteAsync(id);
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