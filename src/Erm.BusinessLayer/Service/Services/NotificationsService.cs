using Erm.DataAccess.Models;
using Erm.DataAccess.Repository;

namespace Erm.BusinessLayer.Service;

public sealed class NotificationService(
    NotificationRepository notificationRepository) : INotificationService
{
    private readonly NotificationRepository _notification = notificationRepository;
	
	public async Task<IEnumerable<Notification>> GetAllAsync(CancellationToken token = default)
		=> await _notification.GetAllAsync(token);

    public async Task<IEnumerable<Notification>> GetBusinessProcessAsync(int id, CancellationToken token = default)
        => await _notification.GetBusinessProcessAsync(id, token);

    public async Task<IEnumerable<Notification>> GetRiskAsync(int id, CancellationToken token = default)
        => await _notification.GetRiskAsync(id, token);

    public async Task<IEnumerable<Notification>> GetRiskProfileAsync(int id, CancellationToken token = default)
        => await _notification.GetRiskProfileAsync(id, token);

    public async Task<Notification> MarkAsReadAsync(int notificationId, CancellationToken token = default)
        => await _notification.MarkAsReadAsync(notificationId, token);
}