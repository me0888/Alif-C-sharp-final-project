using Microsoft.AspNetCore.Mvc;

using Erm.PresentationLayer.WebApi.Authorization;
using Erm.BusinessLayer.Service;
using Erm.BusinessLayer.DTO;

namespace Erm.PresentationLayer.WebApi.Controllers;

[ApiController]
[Route("api/businessprocess")]
public sealed class BusinessProcessController(
    IBusinessProcessService businessProcessService,
    ILogger<BusinessProcessController> logger) : ControllerBase
{
    private readonly IBusinessProcessService _businessProcessService = businessProcessService;
    private readonly ILogger<BusinessProcessController> _logger = logger;

    [HttpPost]
    [ApiKey]
    public async Task<IActionResult> Create([FromBody] BusinessProcessCreateDTO businessProcessDTO)
    {
        try
        {
            var businessProcess = await _businessProcessService.CreateAsync(businessProcessDTO);
            return CreatedAtAction(nameof(GetById), new { id = businessProcess.Id }, businessProcess);
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
            return Ok(await _businessProcessService.GetAllAsync());
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
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            return Ok(await _businessProcessService.GetAsync(id));
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
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BusinessProcessCreateDTO businessProcessDTO)
    {
        try
        {
            return Ok(await _businessProcessService.UpdateAsync(id, businessProcessDTO));
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
    public async Task<IActionResult> UpdatePatch([FromRoute] int id, [FromBody] BusinessProcessUpdateDTO businessProcessUpdateDTO)
    {
        try
        {
            return Ok(await _businessProcessService.UpdatePatchAsync(id, businessProcessUpdateDTO));
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
            await _businessProcessService.DeleteAsync(id);
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