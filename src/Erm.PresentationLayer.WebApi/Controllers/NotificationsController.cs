using Microsoft.AspNetCore.Mvc;

using Erm.PresentationLayer.WebApi.Authorization;
using Erm.BusinessLayer.Service;

namespace Erm.PresentationLayer.WebApi.Controllers;

[ApiController]
[Route("api/notifications")]
public sealed class NotificationController(
	INotificationService notificationsService,
	ILogger<NotificationController> logger) : ControllerBase
{

	private readonly INotificationService _notificationService = notificationsService;
	private readonly ILogger<NotificationController> _logger = logger;
	
	[HttpGet]
	[ApiKey]
	public async Task<IActionResult> GetAll()
		=> Ok(await _notificationService.GetAllAsync());

	[HttpGet]
	[ApiKey]
	[Route("businessprocess/{id}")]
	public async Task<IActionResult> GetBusinessProcessById([FromRoute] int id)
		=> Ok(await _notificationService.GetBusinessProcessAsync(id));

	[HttpGet]
	[ApiKey]
	[Route("risk/{id}")]
	public async Task<IActionResult> GetRiskById([FromRoute] int id)
		=> Ok(await _notificationService.GetRiskAsync(id));

	[HttpGet]
	[ApiKey]
	[Route("riskprofile/{id}")]
	public async Task<IActionResult> GetRiskProfileById([FromRoute] int id)
		=> Ok(await _notificationService.GetRiskProfileAsync(id));

	[HttpPut]
	[ApiKey]
	[Route("{id}/read")]
	public async Task<IActionResult> MarkRiskAsRead([FromRoute] int id)
	{
		try
		{
			return Ok(await _notificationService.MarkAsReadAsync(id));
		}
		catch (InvalidOperationException ex)
		{
			_logger.LogError(ex.Message);
			return NotFound();
		}
	}
}